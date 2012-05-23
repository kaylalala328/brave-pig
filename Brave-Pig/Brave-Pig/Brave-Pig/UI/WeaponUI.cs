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

namespace Brave_Pig.UI
{
    class WeaponUI
    {
        #region Declaration
        Texture2D weapon;

        int count;
        int time = -5;
        float rotate, saveRotate;

        //changeWeapon = 키 입력시 true
        //protectChange 회전 중에는 입력을 받지 못하도록 보호
        bool changeWeapon, protectChange;
        bool drawWeapon;
        #endregion

        #region Initailize
        public void Initialize()
        {
            drawWeapon = false;
            changeWeapon = false;
            protectChange = false;
            rotate = 0.0f;
            saveRotate = 0.0f;
            count = 0;
        }
        #endregion

        #region Load & Update
        public void LoadContent(ContentManager content)
        {
            weapon = content.Load<Texture2D>("UI/weapon");
        }
        public void Update(GameTime gameTime)
        {
            if ( !protectChange )
            {
                if ( Game1.previousKeyState.IsKeyUp(Keys.A) && Game1.currentKeyState.IsKeyDown(Keys.A) )
                {
                    changeWeapon = true;
                    protectChange = true;
                    time = (int)gameTime.TotalGameTime.TotalSeconds;
                    count++;
                }
                //회전 시간 포함 5초 동안만 보여지도록
                if ( time + 5 > gameTime.TotalGameTime.TotalSeconds )
                {
                    drawWeapon = true;
                }
                else
                    drawWeapon = false;
            }
        }
        #endregion

        #region Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            if ( changeWeapon )
            {
                if ( rotate < saveRotate + 2.05f )
                {
                    rotate += 0.1f;
                    spriteBatch.Draw(weapon,
                    new Rectangle(MainUI.width / 20 * 19, MainUI.height / 7 * 5, 120, 104),
                    null,
                    Color.White * 0.7f, rotate, new Vector2(60, 65), SpriteEffects.None, 0f); 
                }
                else
                {
                    changeWeapon = false;
                    protectChange = false;

                    if ( count % 3 == 0 )
                    {
                        count = 0;
                        rotate = 0.0f;
                    }
                }
            }
            if(!changeWeapon)
            {
                if ( drawWeapon )
                {
                    spriteBatch.Draw(weapon,
                        new Rectangle(MainUI.width / 20 * 19, MainUI.height / 7 * 5, 120, 104),
                        null,
                        Color.White * 0.7f, rotate, new Vector2(60, 65), SpriteEffects.None, 0f);
                }
                saveRotate = rotate;
            }
            
        }
        #endregion
    }
}