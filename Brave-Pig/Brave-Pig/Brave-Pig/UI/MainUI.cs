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
        public static SpriteFont UIfont;

        BottomUI bottomUI;
        StatusUI status;

        //모든 UI클래스에서 같이 사용하기 위해...
        public static int width, height;
        #endregion

        #region Initialize
        public void Initialize ( GraphicsDevice graphics )
        {
            width = graphics.Viewport.Width;
            height = graphics.Viewport.Height;

            bottomUI = new BottomUI();
            bottomUI.Initialize();

            status = new StatusUI();
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
