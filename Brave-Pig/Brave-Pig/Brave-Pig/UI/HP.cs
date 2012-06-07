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
    class HP
    {
        Texture2D HPbar;

        int maxHeal;
        float heal;

        public void Initialize ( )
        {
        }
        public void LoadContent ( ContentManager content )
        {
            HPbar = content.Load<Texture2D>("UI/HP");
        }
        public void Update ( GameTime gameTime, Player player )
        {
            heal = player.getHeal();
            maxHeal = player.getMaxHeal();
        }
        public void Draw ( SpriteBatch spriteBatch, SpriteFont UIfont, int BottomUIWidth, int BottomUIHeight, int BottomY )
        {
            int HPbarWidth = ( BottomUIWidth * 151 ) / 1280;
            int HPbarHeight = ( BottomUIHeight * 148 ) / 180;
            int HPbarX = ( BottomUIWidth * 113 ) / 1280;
            int HPbarY = BottomY + ( BottomUIHeight * 17 ) / 180;

            spriteBatch.Draw(HPbar,
                new Rectangle(HPbarX,
                    HPbarY + (int)( ( maxHeal - heal ) * HPbarHeight / maxHeal ),
                    HPbarWidth,
                    (int)( HPbarHeight - ( maxHeal - heal ) * HPbarHeight / maxHeal )),
                new Rectangle(0,
                    (int)( ( maxHeal - heal ) * HPbarHeight / maxHeal ),
                    HPbarWidth,
                    (int)( HPbarHeight - ( maxHeal - heal ) * HPbarHeight / maxHeal )),
                Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);

            spriteBatch.DrawString(UIfont, heal.ToString() + " / " + maxHeal.ToString(), new Vector2(HPbarX + HPbarWidth / 3, HPbarY + HPbarHeight / 3), Color.White);
        }
    }
}
