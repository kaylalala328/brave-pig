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
    class MP : GameObject
    {
        int manaLevel = 0;
        float mana;

        Texture2D chargeBar;

        public void Initialize ( )
        {
        }
        public void LoadContent ( ContentManager content )
        {
            chargeBar = content.Load<Texture2D>("UI/chargeBar");
            IsEnemy = true;
            animations.Add("normal", new AnimationStrip(content.Load<Texture2D>("UI/MP"), 44, "normal"));
            animations["normal"].LoopAnimation = true;
            animations["normal"].FrameLength = 0.08f;
            animations.Add("event", new AnimationStrip(content.Load<Texture2D>("UI/MPanimation3"), 44, "event"));
            animations["event"].LoopAnimation = false;
            animations["event"].FrameLength = 0.1f;

            currentAnimation = "event";
            PlayAnimation("event");
        }
        public void Update ( GameTime gameTime, Player player )
        {
            mana = player.getMana();

            //ChargeBar의 계산을 위해서 필요
            if ( mana < 1 )
                manaLevel = 0;
            else if ( mana >= 1 && mana < 2 && manaLevel < 1 )
            {
                manaLevel = 1;
                PlayAnimation("event");
                updateAnimation(gameTime);
            }
            else if ( mana >= 2 && mana < 3 && manaLevel < 2 )
            {
                manaLevel = 2;
                PlayAnimation("event");
                updateAnimation(gameTime);
            }
            else if ( mana >= 3 && manaLevel < 3 )
            {
                manaLevel = 3;
                PlayAnimation("event");
                updateAnimation(gameTime);
            }
            else
            {
                manaLevel = (int)mana;
                updateAnimation(gameTime);
            }
        }
        public void Draw ( SpriteBatch spriteBatch, int BottomUIWidth, int BottomUIHeight, int BottomY )
        {
            int MPbarWidth = ( BottomUIWidth * 44 ) / 1280;
            int MPbarHeight = ( BottomUIHeight * 44 ) / 180;
            int MPbarX = ( BottomUIWidth * 1061 ) / 1280;
            int MPbarY = BottomY + ( BottomUIHeight * 118 ) / 180;

            if ( animations.ContainsKey(currentAnimation) )
            {
                //현재 spriteBatch를 그린다.
                for ( int i = 1 ; i < 4 ; i++ )
                {
                    if ( manaLevel >= i )
                    {
                        MPbarY = BottomY + ( BottomUIHeight * ( 118 - ( ( i - 1 ) * 51 ) ) ) / 180;
                        spriteBatch.Draw(
                        animations[currentAnimation].Texture,
                        new Rectangle(MPbarX, MPbarY, MPbarWidth, MPbarHeight),
                        animations[currentAnimation].FrameRectangle,
                        Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0f);
                    }
                }
                //animation이 끝났을 경우
                if ( animations[currentAnimation].FinishedPlaying == true )
                    PlayAnimation("normal");
            }
            DrawCharge(spriteBatch, BottomUIWidth, BottomUIHeight, BottomY);

            //Test용 마나량 출력
            //spriteBatch.DrawString(MainUI.UIfont, mana.ToString(), new Vector2(100, 100), Color.Black);
        }
        public void DrawCharge ( SpriteBatch spriteBatch, int BottomUIWidth, int BottomUIHeight, int BottomY )
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
    }
}
