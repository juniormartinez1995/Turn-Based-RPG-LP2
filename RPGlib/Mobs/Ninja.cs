using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGlib.Mobs
{
    public class Ninja : Mob
    {
        public Ninja()
        {
            this.name = "Ninja Ninja";
            this.HP = 200;
            this.Mana = 50;
            this.evasionRate = 20;
            this.currentArmor = 10;
            this.Damage = 100;
            this.GifBattle = null;

        }
    }
}
