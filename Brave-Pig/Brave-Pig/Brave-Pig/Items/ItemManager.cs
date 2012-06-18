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

        public static List<string> haveSwords =
            new List<string>();
        public static List<string> haveArmors =
            new List<string>();

        private static Potion potion;

        private static string currentSword;
        private static string currentArmor;

        public static void Initialize(ContentManager content)
        {
            swords.Add("Basic", new Sword(0, 10, 15, 20, 25));
            swords.Add("Blue", new Sword(0, 15, 20, 25, 30, content.Load<Texture2D>("Item/sword1")));
            swords.Add("Red", new Sword(0, 20, 25, 30, 35, content.Load<Texture2D>("Item/sword2")));
            swords.Add("Yellow", new Sword(0, 25, 30, 35, 40, content.Load<Texture2D>("Item/sword3")));

            armors.Add("Armor", new Armor(0, 10, content.Load<Texture2D>("Item/armor")));
            armors.Add("Boots", new Armor(0, 10, content.Load<Texture2D>("Item/boots")));
            armors.Add("Shield", new Armor(0, 10,content.Load<Texture2D>("Item/shield")));

            potion = new Potion(25, 10);

            currentSword = "Basic";
            currentArmor = "none";
        }

        public static string getCurrentSword()
        {
            return currentSword;
        }
        public static string getCurrentArmor()
        {
            return currentArmor;
        }
        public static void setCurrentSword ( string newItem )
        {
            currentSword = newItem;
        }
        public static void setCurrnetArmor(string newitem)
        {
            currentArmor = newitem;
        }
        public static void gainSword(string newSword)
        {
            foreach (string sw in haveSwords)
            {
                if (sw == newSword)
                    return;
            }
            haveSwords.Add(newSword);
        }
        public static void gainArmor ( string newArmor )
        {
            foreach (string am in haveArmors)
            {
                if (am == newArmor)
                    return;
            }
            haveArmors.Add(newArmor);
            setCurrnetArmor(newArmor);
        }
        public static Potion getPotion()
        {
            return potion;
        }
        public static void remove ( )
        {
            for ( int i = 0 ; i < haveSwords.Count() ; i++ )
            {
                haveSwords.RemoveAt(i);
            }
            for ( int i = 0 ; i < haveArmors.Count() ; i++ )
            {
                haveArmors.RemoveAt(i);
            }
            setCurrentSword("Basic");
            setCurrnetArmor("none");
        }
    }
}