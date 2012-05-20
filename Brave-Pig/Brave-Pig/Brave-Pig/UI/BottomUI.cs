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
    class BottomUI
    {
        Texture2D bottomUI;
        Texture2D weapon;
        int width, height;

        bool changeWeapon;
        float rotate, rotate1, rotate2, rotate3;

        public void Initialize(GraphicsDevice graphics)
        {
            width = graphics.Viewport.Width;
            height = graphics.Viewport.Height;

            changeWeapon = false;
            rotate = 0.0f;
        }
        public void LoadContent(ContentManager content)
        {
            bottomUI = content.Load<Texture2D>("UI/BottomUI");
            weapon = content.Load<Texture2D>("UI/weapon");
        }
        public void Update(GameTime gameTime)
        {
            if (Game1.previousKeyState.IsKeyDown(Keys.A) && Game1.currentKeyState.IsKeyUp(Keys.A))
            {
                changeWeapon = true;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bottomUI,
                new Rectangle(0, height / 4 * 3, width, height / 4),
                Color.White);
            if (changeWeapon)
            {
                if (rotate < rotate1 + 2.05f)
                {
                    rotate += 0.1f;
                    spriteBatch.Draw(weapon,
                    new Rectangle(width / 8 * 7, height / 7 * 6, 80, 80),
                    null,
                    Color.White, rotate, new Vector2(80, 80), SpriteEffects.None, 0f);
                }
                else
                {
                    changeWeapon = false;
                }
            }
            else
            {
                spriteBatch.Draw(weapon,
                    new Rectangle(width / 8 * 7, height / 7 * 6, 80, 80),
                    null,
                    Color.White, rotate, new Vector2(80, 80), SpriteEffects.None, 0f);
                rotate1 = rotate;
            }
        }
    }
}
