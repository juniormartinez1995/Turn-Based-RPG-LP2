using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGlib.Characters
{
    public class Berserker : Character
    {

        public void createBerseker()
        {
            this.currentHP = 250;
            this.currentMana = 100;
            this.currentXP = 0;
            this.Level = 0;
            this.criticRate = 15;
            this.evasionRate = 5;
            this.currentArmor = 20;
        }

        public bool upLevel()
        {
            if ((this.currentXP + this.gainedXP) > 100)
            {
                this.currentXP = 0;
                this.Level += 1;
                return true;

            }
            return false;
        }

        public bool isDead()
        {
            if (this.currentHP <= 0) return true;
            else return false;
        }

        public bool countCritic()
        {

            Random random = new Random();
            int criticCalc = random.Next(0, 100);
            if (criticCalc <= this.criticRate)
            {
                return true;
            }
            else return false;
        }

    }
}
