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
    class Status
    {
        Texture2D status;
        int width, height;
        int attack, defense;

        public void Initialize(GraphicsDevice graphics)
        {
            width = graphics.Viewport.Width;
            height = graphics.Viewport.Height;
        }
        public void LoadContent(ContentManager content)
        {
            status = content.Load<Texture2D>("UI/status");
        }
        public void Update(GameTime gameTime, Player player)
        {
            attack = player.getAttack();
            defense = player.getDefense();
        }
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(status,
                new Rectangle(0, 0, width / 6, height / 6),
                null,
                Color.White * 0.7f, 0f, Vector2.Zero, SpriteEffects.None, 0f);

            spriteBatch.DrawString(font, "ATT " + attack.ToString(), new Vector2(105, 40), Color.Black);
            spriteBatch.DrawString(font, "DEF " + defense.ToString(), new Vector2(105, 70), Color.Black);
        }
    }
}
