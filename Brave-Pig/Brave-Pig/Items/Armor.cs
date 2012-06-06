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

namespace Brave_Pig.Items
{
    public class Armor
    {
        int itemNum;
        int defense;
        Texture2D ArmorImage;

        public Armor(int num, int def, Texture2D image)
        {
            itemNum = num;
            defense = def;

            ArmorImage = image;
        }

        public int getArmor()
        {
            return defense;
        }
        public Texture2D getImage()
        {
            return ArmorImage;
        }
    }
}
