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
        int width, height;

        WeaponUI weapon;

        public void Initialize(GraphicsDevice graphics)
        {
            width = graphics.Viewport.Width;
            height = graphics.Viewport.Height;

            weapon = new WeaponUI();
            weapon.Initialize(graphics);
        }
        public void LoadContent(ContentManager content)
        {
            bottomUI = content.Load<Texture2D>("UI/BottomUI");

            weapon.LoadContent(content);
        }
        public void Update(GameTime gameTime)
        {
            weapon.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bottomUI,
                new Rectangle(0, height / 4 * 3, width, height / 4),
                Color.White);

            weapon.Draw(spriteBatch);
        }
    }
}
