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
    public static class ItemManager
    {
        public static Dictionary<string, Sword> swords =
            new Dictionary<string, Sword>();
        public static Dictionary<string, Armor> armors =
            new Dictionary<string, Armor>();

        public static List<Sword> haveSwords =
            new List<Sword>();
        public static List<Armor> haveArmors =
            new List<Armor>();

        private static string currentSword;

        public static void Initialize()
        {
            swords.Add("Basic", new Sword(0, 10, 15, 20, 25));
            swords.Add("Blue", new Sword(0, 15, 20, 25, 30));
            swords.Add("Red", new Sword(0, 20, 25, 30, 35));
            swords.Add("Yellow", new Sword(0, 25, 30, 35, 40));

            armors.Add("Armor", new Armor(0, 10));
            armors.Add("Boots", new Armor(0, 10));
            armors.Add("Shield", new Armor(0, 10));

            currentSword = "Basic";
        }

        public static string getCurrentSword()
        {
            return currentSword;
        }
        public static void setCurrentSword ( string newItem )
        {
            currentSword = newItem;
        }
        public static void gainSword(string newSword)
        {
            haveSwords.Add(swords[newSword]);
        }
        public static void gainArmor ( string newArmor )
        {
            haveArmors.Add(armors[newArmor]);
        }
    }
}