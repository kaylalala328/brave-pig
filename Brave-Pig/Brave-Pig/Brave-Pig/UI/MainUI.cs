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

namespace Brave_Pig.UI
{
    class MainUI
    {
        BottomUI bottomUI;
        Status status;

        public void Initialize(GraphicsDevice graphics)
        {
            bottomUI = new BottomUI();
            bottomUI.Initialize(graphics);

            status = new Status();
            status.Initialize(graphics);
        }
        public void LoadContent(ContentManager content)
        {
            bottomUI.LoadContent(content);
            status.LoadContent(content);
        }
        public void Update(GameTime gameTime, Player player)
        {
            bottomUI.Update(gameTime, player);
            status.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            bottomUI.Draw(spriteBatch);
            status.Draw(spriteBatch);
        }
    }
}
