﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGlib.Characters
{
    abstract public class Character
    {
        public String name { get; set; }
        public int currentHP { get; set; }
        public int maxHealth { get; set; }
        public int maxMana { get; set; }
        public int currentMana { get; set; }
        public int currentArmor { get; set; }
        public int evasionRate { get; set; }
        public int criticRate { get; set; }
        public int currentXP { get; set; }
        public int gainedXP { get; set; }
        public int Level { get; set; }

    }
}