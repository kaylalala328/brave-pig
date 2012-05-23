﻿using System;
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

        int width, height, count;
        float rotate, saveRotate;
        bool changeWeapon, protectChange;
        #endregion

        #region Initailize
        public void Initialize(GraphicsDevice graphics)
        {
            width = graphics.Viewport.Width;
            height = graphics.Viewport.Height;

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
                if ( Game1.previousKeyState.IsKeyDown(Keys.A) && Game1.currentKeyState.IsKeyUp(Keys.A) )
                {
                    changeWeapon = true;
                    protectChange = true;
                    count++;
                }
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
                    new Rectangle(width / 20 * 19, height / 7 * 5, 120, 104),
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
            else
            {
                spriteBatch.Draw(weapon,
                    new Rectangle(width / 20 * 19, height / 7 * 5, 120, 104),
                    null,
                    Color.White * 0.7f, rotate, new Vector2(60, 65), SpriteEffects.None, 0f);
                saveRotate = rotate;
            }
        }
        #endregion
    }
}
