﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Brave_Pig.Character
{
    public class Status
    {
        //케릭터에 사용될 status
        public int healPoint;
        public int manaPoint;
        public int damage;
        public int defense;
        public int useSword;

        //생성자 및 새터, 게터
        public int HealPoint
        {
            get { return healPoint; }
            set { healPoint = 50; }
        }

        public int ManaPoint
        {
            get { return manaPoint; }
            set { healPoint = 30; }
        }

        public int Damage
        {
            get { return damage; }
            set { damage = 10; }
        }

        public int Defense
        {
            get { return defense; }
            set { defense = 5; }
        }

        public int UseSword
        {
            get { return useSword; }
        }
        public Status()
        {
            this.healPoint = 0;
            this.manaPoint = 0;
            this.damage = 0;
            this.defense = 0;
            this.useSword = 0;
        }
        public Status(int heal, int mana, int damage, int defense, int use)
        {
            this.healPoint = heal;
            this.manaPoint = mana;
            this.damage = damage;
            this.defense = defense;
            this.useSword = use;
        }
    }
}
