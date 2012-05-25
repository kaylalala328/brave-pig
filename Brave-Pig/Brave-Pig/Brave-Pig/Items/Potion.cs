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
    public class Potion
    {
        int heal;
        int count;

        public Potion(int healing, int num)
        {
            heal = healing;
            count = num;
        }
        public int getHeal()
        {
            return heal;
        }
        public int getCount()
        {
            return count;
        }
        public void usePotion()
        {
            count--;
        }
    }
}
