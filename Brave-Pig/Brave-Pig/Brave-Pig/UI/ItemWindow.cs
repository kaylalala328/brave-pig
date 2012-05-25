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
        Texture2D skill1, skill2;

        public void LoadContent(ContentManager content)
        {
            skill1 = content.Load<Texture2D>("UI/skill1");
            skill2 = content.Load<Texture2D>("UI/skill2");
        }
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
        public void DrawSkill ( SpriteBatch spriteBatch, int BottomUIWidth, int BottomUIHeight, int BottomY )
        {
            int Width = ( BottomUIWidth * 70 ) / 1280;
            int Height = ( BottomUIHeight * 66 ) / 180;
            int X = ( BottomUIWidth * 773 ) / 1280;
            int Y = BottomY + ( BottomUIHeight * 81 ) / 180;

            if ( ItemManager.getCurrentSword() == "Basic" )
            {
                spriteBatch.Draw(skill1,
                    new Rectangle(X, Y, Width, Height),
                    Color.White);
            }
            else
            {
                spriteBatch.Draw(skill2,
                    new Rectangle(X, Y, Width, Height),
                    Color.White);
            }
        }
        public void DrawPotion ( SpriteBatch spriteBatch, int BottomUIWidth, int BottomUIHeight, int BottomY )
        {
            int X = ( BottomUIWidth * 725 ) / 1280;
            int Y = BottomY + ( BottomUIHeight * 120 ) / 180;

            spriteBatch.DrawString(MainUI.UIfont, 
                ItemManager.getPotion().getCount().ToString(),
                new Vector2(X, Y),
                Color.White);
        }
    }
}