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
using Brave_Pig.Items;

///Sword & Armor Item Draw
namespace Brave_Pig.UI
{
    class ItemWindow
    {
        public void DrawSword(SpriteBatch spriteBatch, int BottomUIWidth, int BottomUIHeight, int BottomY)
        {
            int Width = ( BottomUIWidth * 70 ) / 1280;
            int Height = (BottomUIHeight * 66) / 180;
            int X = ( BottomUIWidth * 284 ) / 1280;
            int Y = BottomY + (BottomUIHeight * 81) / 180;

            for ( int i = 0 ; i < ItemManager.haveSwords.Count ; i++ )
            {
                spriteBatch.Draw(ItemManager.swords[ItemManager.haveSwords[i]].getImage(),
                    new Rectangle(X, Y, Width, Height),
                    Color.White);

                X = X + ( BottomUIWidth * 67 ) / 1280;
            }
        }
        public void DrawArmor ( SpriteBatch spriteBatch, int BottomUIWidth, int BottomUIHeight, int BottomY )
        {
            int Width = ( BottomUIWidth * 70 ) / 1280;
            int Height = ( BottomUIHeight * 66 ) / 180;
            int X = ( BottomUIWidth * 485 ) / 1280;
            int Y = BottomY + ( BottomUIHeight * 81 ) / 180;

            for ( int i = 0 ; i < ItemManager.haveArmors.Count ; i++ )
            {
                spriteBatch.Draw(ItemManager.armors[ItemManager.haveArmors[i]].getImage(),
                    new Rectangle(X, Y, Width, Height),
                    Color.White);

                X = X + ( BottomUIWidth * 67 ) / 1280;
            }
        }
    }
}
