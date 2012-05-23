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
    class BottomUI : UiAnimation
    {
        #region Declaration
        Texture2D bottomUI;

        WeaponUI weapon;
        HP hp;
        MP mp;

        #endregion

        #region Initialize & Update
        public void Initialize ()
        {
            weapon = new WeaponUI();
            weapon.Initialize();

            hp = new HP();
            mp = new MP();
        }
        public void LoadContent ( ContentManager content )
        {
            bottomUI = content.Load<Texture2D>("UI/Bottom");

            weapon.LoadContent(content);
            hp.LoadContent(content);
            mp.LoadContent(content);
        }
        public void Update ( GameTime gameTime, Player player)
        {
            weapon.Update(gameTime);
            hp.Update(gameTime, player);
            mp.Update(gameTime, player);
        }
        #endregion

        #region Draw
        public void Draw ( SpriteBatch spriteBatch, SpriteFont UIfont )
        {
            int BottomUIWidth = MainUI.width;
            int BottomUIHeight = MainUI.height / 4;
            int BottomY = MainUI.height / 4 * 3;

            spriteBatch.Draw(bottomUI,
                new Rectangle(0, BottomY, BottomUIWidth, BottomUIHeight),
                Color.White);

            hp.Draw(spriteBatch, UIfont, BottomUIWidth, BottomUIHeight, BottomY);
            mp.Draw(spriteBatch, BottomUIWidth, BottomUIHeight, BottomY);
            weapon.Draw(spriteBatch);
        }
        #endregion
    }
}
