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
    class StatusUI
    {
        #region Declaration
        Texture2D status;
        int width, height;
        int attack, defense;
        #endregion

        #region Initailize
        public void Initialize(GraphicsDevice graphics)
        {
            width = graphics.Viewport.Width;
            height = graphics.Viewport.Height;
        }
        #endregion

        #region Load & Update
        public void LoadContent(ContentManager content)
        {
            status = content.Load<Texture2D>("UI/status");
        }
        public void Update(GameTime gameTime, Player player)
        {
            attack = player.getAttack();
            defense = player.getDefense();
        }
        #endregion

        #region Draw
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(status,
                new Rectangle(0, 0, width / 8, height / 8),
                Color.White * 0.7f);

            spriteBatch.DrawString(font, "ATT " + attack.ToString(), new Vector2(90, 30), Color.Black);
            spriteBatch.DrawString(font, "DEF " + defense.ToString(), new Vector2(90, 60), Color.Black);
        }
        #endregion
    }
}
