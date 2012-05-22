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
        Texture2D bottomUI;
        Texture2D HPbar;
        Texture2D MPbar;
        Texture2D chargeBar;
        int width, height;
        float heal, mana;
        int manaLevel;
        int maxHeal;

        SpriteFont UIfont;

        WeaponUI weapon;

        protected Dictionary<string, AnimationStrip> animations =
            new Dictionary<string, AnimationStrip>();
        protected string currentAnimation;

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
            MPbar = content.Load<Texture2D>("UI/MP");
            chargeBar = content.Load<Texture2D>("UI/chargeBar");
            UIfont = content.Load<SpriteFont>("Font/UI font");

            weapon.LoadContent(content);

            animations.Add("normal", new AnimationStrip(content.Load<Texture2D>("UI/MPanimation"), 44, "normal"));
            animations["normal"].LoopAnimation = true;
            animations["normal"].FrameLength = 0.08f;

            currentAnimation = "normal";
            PlayAnimation("normal");
        }
        public void Update ( GameTime gameTime, Player player)
        {
            heal = player.getHeal();
            mana = player.getMana();
            maxHeal = player.getMaxHeal();
            weapon.Update(gameTime);
            updateAnimation(gameTime);
            //ChargeBar의 계산을 위해서 필요
            if ( mana < 1 )
                manaLevel = 0;
            else if ( mana < 2 )
                manaLevel = 1;
            else if ( mana < 3 )
                manaLevel = 2;
            else
                manaLevel = 3;
        }
        public void Draw ( SpriteBatch spriteBatch )
        {
            int BottomUIWidth = width;
            int BottomUIHeight = height / 4;
            int BottomY = height / 4 * 3;

            spriteBatch.Draw(bottomUI,
                new Rectangle(0, BottomY, BottomUIWidth, BottomUIHeight),
                Color.White);

            int HPbarWidth = (BottomUIWidth / 1280) * 151;
            int HPbarHeight = (BottomUIHeight / 180) * 148;
            int HPbarX = (BottomUIWidth / 1280) * 113;
            int HPbarY = BottomY + ( BottomUIHeight / 180 ) * 17;

            DrawHP(spriteBatch, HPbarWidth, HPbarHeight, HPbarX, HPbarY);

            int chargeBarWidth = (BottomUIWidth / 1280) * 713;
            int chargeBarHeight = (BottomUIHeight / 180) * 10;
            int chargeBarX = (BottomUIWidth / 1280) * 275;
            int chargeBarY = BottomY + ( BottomUIHeight / 180 ) * 67;
            DrawCharge(spriteBatch, chargeBarWidth, chargeBarHeight, chargeBarX, chargeBarY);

            int MPbarWidth = ( BottomUIWidth / 1280 ) * 44;
            int MPbarHeight = ( BottomUIHeight / 180 ) * 44;
            int MPbarX = ( BottomUIWidth / 1280 ) * 1061;
            int MPbarY = BottomY + ( BottomUIHeight / 180 ) * 118;

            DrawMP(spriteBatch, MPbarWidth, MPbarHeight, MPbarX, MPbarY, BottomUIHeight, BottomY);

            //Test용 마나량 출력
            spriteBatch.DrawString(UIfont, mana.ToString(), new Vector2(100, 100), Color.Black);

            weapon.Draw(spriteBatch);
        }

        public void DrawHP(SpriteBatch spriteBatch, int HPbarWidth, int HPbarHeight, int HPbarX, int HPbarY)
        {
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
        }
        public void DrawCharge(SpriteBatch spriteBatch, int chargeBarWidth, 
            int chargeBarHeight, int chargeBarX, int chargeBarY)
        {
            spriteBatch.Draw(chargeBar,
                new Rectangle(chargeBarX,
                    chargeBarY,
                    (int)( 713 * ( mana - manaLevel ) ),
                    chargeBarHeight),
                new Rectangle(0, 0, (int)( chargeBarWidth * ( mana - manaLevel ) ), chargeBarHeight),
                Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }

        public void DrawMP ( SpriteBatch spriteBatch, int MPbarWidth, int MPbarHeight, int MPbarX, int MPbarY,
            int BottomUIHeight, int BottomY)
        {
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
            }
        }

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
        
    }
}
