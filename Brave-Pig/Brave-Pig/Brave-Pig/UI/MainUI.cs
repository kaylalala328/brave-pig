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
        #region Declaration
        SpriteFont UIfont;

        BottomUI bottomUI;
        StatusUI status;
        #endregion

        #region Initialize
        public void Initialize ( GraphicsDevice graphics )
        {
            bottomUI = new BottomUI();
            bottomUI.Initialize(graphics);

            status = new StatusUI();
            status.Initialize(graphics);
        }
        #endregion

        #region Load & Update
        public void LoadContent ( ContentManager content )
        {
            bottomUI.LoadContent(content);
            status.LoadContent(content);

            UIfont = content.Load<SpriteFont>("Font/UI font");
        }
        public void Update ( GameTime gameTime, Player player )
        {
            bottomUI.Update(gameTime, player);
            status.Update(gameTime, player);
        }
        #endregion

        #region Draw
        public void Draw ( SpriteBatch spriteBatch )
        {
            bottomUI.Draw(spriteBatch, UIfont);
            status.Draw(spriteBatch, UIfont);
        }
        #endregion
    }
}
