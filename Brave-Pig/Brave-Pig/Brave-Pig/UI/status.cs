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
    class Status
    {
        Texture2D status;
        int width, height;

        bool OpenStatus;

        public void Initialize(GraphicsDevice graphics)
        {
            width = graphics.Viewport.Width;
            height = graphics.Viewport.Height;

            OpenStatus = false;
        }
        public void LoadContent(ContentManager content)
        {
            status = content.Load<Texture2D>("UI/status");
        }
        public void Update(GameTime gameTime)
        {
            if (Game1.previousKeyState.IsKeyDown(Keys.I) && Game1.currentKeyState.IsKeyUp(Keys.I))
            {
                if (OpenStatus)
                    OpenStatus = false;
                else
                    OpenStatus = true;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (OpenStatus)
            {
                spriteBatch.Draw(status,
                    new Rectangle(width / 8, height / 6, width / 4 * 3, height / 3 * 2),
                    Color.White);
            }
        }
    }
}
