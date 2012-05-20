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

        public void Initialize(GraphicsDevice graphics)
        {
            width = graphics.Viewport.Width;
            height = graphics.Viewport.Height;
        }
        public void LoadContent(ContentManager content)
        {
            status = content.Load<Texture2D>("UI/status");
        }
        public void Update(GameTime gameTime)
        {
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(status,
                new Rectangle(0, 0, width / 6, height / 6),
                null,
                Color.White * 0.5f, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }
    }
}
