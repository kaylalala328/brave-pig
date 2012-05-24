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
    public class Sword
    {
        int itemNum;
        int attack;
        int skill1;
        int skill2;
        int skill3;
        Texture2D swordImage;

        public Sword ( int num, int att, int skillDam1, int skillDam2, int skillDam3 )
        {
            itemNum = num;
            attack = att;
            skill1 = skillDam1;
            skill2 = skillDam2;
            skill3 = skillDam3;
        }
        public Sword ( int num, int att, int skillDam1, int skillDam2, int skillDam3, Texture2D image )
        {
            itemNum = num;
            attack = att;
            skill1 = skillDam1;
            skill2 = skillDam2;
            skill3 = skillDam3;
            swordImage = image;
        }

        public int getSword ( )
        {
            return attack;
        }
        public int getSkill1 ( )
        {
            return skill1;
        }
        public int getSkill2 ( )
        {
            return skill2;
        }
        public int getSkill3 ( )
        {
            return skill3;
        }
        public Texture2D getImage()
        {
            return swordImage;
        }
    }
}