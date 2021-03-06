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

namespace Brave_Pig
{
    class make
    {
        static Texture2D makepeople;

        public static void Initialize(GraphicsDevice graphics)
        {
        }

        public static void LoadContent(ContentManager content)
        {
            makepeople = content.Load<Texture2D>("Screen/makepeople");
        }

        public static void Update(GameTime gameTime)
        {
            if (Game1.currentKeyState.IsKeyDown(Keys.Escape))
            {
                Game1.gameState = Game1.GameStates.START;
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(makepeople,
                new Rectangle(0, 0, 1280, 720),
                Color.White);
        }
    }
}
