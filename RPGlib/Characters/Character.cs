using System;
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
        private int maxHealth { get; set; }
        protected int manaMax { get; set; }
        protected int mana { get; set; }
        protected int armor { get; set; }
        protected int evasion { get; set; }
        protected int critic { get; set; }
        protected int currentXP { get; set; }
        protected int gainedXP { get; set; }
        protected int Level { get; set; }

    }
}
