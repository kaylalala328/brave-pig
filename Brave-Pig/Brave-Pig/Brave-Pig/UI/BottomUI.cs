using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Brave_Pig.Character;
using Brave_Pig.BasicObject;
using Brave_Pig.Monsters;

namespace Brave_Pig.UI
{
    class BottomUI
    {
        #region Declaration
        Texture2D bottomUI;
        Texture2D HPbar;
        Texture2D chargeBar;
        int width, height;
        float heal, mana;
        int manaLevel;
        int maxHeal;

        WeaponUI weapon;

        protected Dictionary<string, AnimationStrip> animations =
            new Dictionary<string, AnimationStrip>();
        protected string currentAnimation;
        #endregion

        #region Initialize & Update
        public void Initialize ( GraphicsDevice graphics )
        {
            width = graphics.Viewport.Width;
            height = graphics.Viewport.Height;

            weapon = new WeaponUI();
            weapon.Initialize(graphics);

            manaLevel = 0;
        }
        public void LoadContent ( ContentManager content )
        {
            bottomUI = content.Load<Texture2D>("UI/Bottom");
            HPbar = content.Load<Texture2D>("UI/HP");
            chargeBar = content.Load<Texture2D>("UI/chargeBar");

            weapon.LoadContent(content);

            animations.Add("normal", new AnimationStrip(content.Load<Texture2D>("UI/MP1"), 44, "normal"));
            animations["normal"].LoopAnimation = true;
            animations["normal"].FrameLength = 0.08f;
            animations.Add("event", new AnimationStrip(content.Load<Texture2D>("UI/MPanimation3"), 44, "event"));
            animations["event"].LoopAnimation = false;
            animations["event"].FrameLength = 0.1f;

            currentAnimation = "event";
            PlayAnimation("event");
        }
        public void Update ( GameTime gameTime, Player player)
        {
            heal = player.getHeal();
            mana = player.getMana();
            maxHeal = player.getMaxHeal();
            weapon.Update(gameTime);
            
            //ChargeBar의 계산을 위해서 필요
            if ( mana <= 1 )
                manaLevel = 0;
            else if ( mana < 2  && manaLevel != 1)
            {
                manaLevel = 1;
                updateAnimation(gameTime);
            }
            else if ( mana >=2 && mana < 3 && manaLevel != 2)
            {
                manaLevel = 2;
                PlayAnimation("event");
                updateAnimation(gameTime);
            }
            else if(mana >= 3 && manaLevel != 3)
            {
                manaLevel = 3;
                PlayAnimation("event");
                updateAnimation(gameTime);
            }
            else
            {
                updateAnimation(gameTime);
            }
        }
        #endregion

        #region Draw
        public void Draw ( SpriteBatch spriteBatch, SpriteFont UIfont )
        {
            int BottomUIWidth = width;
            int BottomUIHeight = height / 4;
            int BottomY = height / 4 * 3;

            spriteBatch.Draw(bottomUI,
                new Rectangle(0, BottomY, BottomUIWidth, BottomUIHeight),
                Color.White);

            DrawHP(spriteBatch, UIfont, BottomUIWidth, BottomUIHeight, BottomY);
            DrawCharge(spriteBatch, BottomUIWidth, BottomUIHeight, BottomY);
            DrawMP(spriteBatch, BottomUIWidth, BottomUIHeight, BottomY);
            weapon.Draw(spriteBatch);
            //Test용 마나량 출력
            spriteBatch.DrawString(UIfont, mana.ToString(), new Vector2(100, 100), Color.Black);
            
        }
        public void DrawHP(SpriteBatch spriteBatch, SpriteFont UIfont, int BottomUIWidth, int BottomUIHeight, int BottomY)
        {
            int HPbarWidth = ( BottomUIWidth * 151 ) / 1280;
            int HPbarHeight = ( BottomUIHeight * 148 ) / 180;
            int HPbarX = ( BottomUIWidth * 113 ) / 1280;
            int HPbarY = BottomY + ( BottomUIHeight * 17 ) / 180;

            spriteBatch.Draw(HPbar,
                new Rectangle(HPbarX,
                    HPbarY + (int)( ( maxHeal - heal ) * HPbarHeight / maxHeal ),
                    HPbarWidth,
                    (int)( HPbarHeight - ( maxHeal - heal ) * HPbarHeight / maxHeal )),
                new Rectangle(0,
                    (int)( ( maxHeal - heal ) * HPbarHeight / maxHeal ),
                    151,
                    (int)( HPbarHeight - ( maxHeal - heal ) * HPbarHeight / maxHeal )),
                Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);

            spriteBatch.DrawString(UIfont, heal.ToString() + " / " + maxHeal.ToString(), new Vector2(HPbarX + HPbarWidth / 3, HPbarY + HPbarHeight / 3), Color.White);
        }
        public void DrawCharge(SpriteBatch spriteBatch, int BottomUIWidth, int BottomUIHeight, int BottomY)
        {
            int chargeBarWidth = ( BottomUIWidth * 713 ) / 1280;
            int chargeBarHeight = ( BottomUIHeight * 10 ) / 180;
            int chargeBarX = ( BottomUIWidth * 275 ) / 1280;
            int chargeBarY = BottomY + ( BottomUIHeight * 67 ) / 180;

            spriteBatch.Draw(chargeBar,
                new Rectangle(chargeBarX,
                    chargeBarY,
                    (int)( chargeBarWidth * ( mana - manaLevel ) ),
                    chargeBarHeight),
                new Rectangle(0, 0, (int)( chargeBarWidth * ( mana - manaLevel ) ), chargeBarHeight),
                Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }
        public void DrawMP ( SpriteBatch spriteBatch, int BottomUIWidth, int BottomUIHeight, int BottomY)
        {
            int MPbarWidth = ( BottomUIWidth * 44 ) / 1280;
            int MPbarHeight = ( BottomUIHeight * 44 ) / 180;
            int MPbarX = ( BottomUIWidth * 1061 ) / 1280;
            int MPbarY = BottomY + ( BottomUIHeight * 118 ) / 180;

            if ( animations.ContainsKey(currentAnimation) )
            {
                //현재 spriteBatch를 그린다.
                if ( manaLevel >= 1 )
                {
                    MPbarY = BottomY + ( BottomUIHeight / 180 ) * 118;
                    spriteBatch.Draw(
                    animations[currentAnimation].Texture,
                    new Rectangle(MPbarX, MPbarY, MPbarWidth, MPbarHeight),
                    animations[currentAnimation].FrameRectangle,
                    Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0f);
                }
                if ( manaLevel >= 2 )
                {
                    MPbarY = BottomY + ( BottomUIHeight / 180 ) * 67;
                    spriteBatch.Draw(
                    animations[currentAnimation].Texture,
                    new Rectangle(MPbarX, MPbarY, MPbarWidth, MPbarHeight),
                    animations[currentAnimation].FrameRectangle,
                    Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0f);
                }
                if ( manaLevel >= 3 )
                {
                    MPbarY = BottomY + ( BottomUIHeight / 180 ) * 16;
                    spriteBatch.Draw(
                    animations[currentAnimation].Texture,
                    new Rectangle(MPbarX, MPbarY, MPbarWidth, MPbarHeight),
                    animations[currentAnimation].FrameRectangle,
                    Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0f);
                }
                //animation이 끝났을 경우
                if ( animations[currentAnimation].FinishedPlaying == true )
                    PlayAnimation("normal");
            }
        }
        #endregion

        #region Animation
        private void updateAnimation ( GameTime gameTime )
        {
            //Key를 갖고 잇어야함. 즉 애니메이션이 존재하면,
            if ( animations.ContainsKey(currentAnimation) )
            {
                //애니메이션이 끝났다면
                if ( animations[currentAnimation].FinishedPlaying )
                {
                    //애니메이션은 다음 애니메이션을 가르키게 한다.
                    PlayAnimation(animations[currentAnimation].NextAnimation);
                }
                else
                {
                    //애니메이션이 안끝낫다면
                    animations[currentAnimation].Update(gameTime);
                }
            }
        }
        public void PlayAnimation ( string name )
        {
            if ( !( name == null ) && animations.ContainsKey(name) )
            {
                currentAnimation = name;
                animations[name].Play();
            }
        }
        #endregion
    }
}
