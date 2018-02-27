using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_LP2
{
    abstract class Characters
    {
        protected String name { get; set; }
        protected int life { get; set; }
        protected int mana { get; set; }
        protected int armor { get; set; }
        protected int evasion { get; set; }
        protected int critic { get; set; }
        protected int currentXP { get; set; }
        protected int gainedXP { get; set; }
        protected int Level { get; set; }

    }
}
