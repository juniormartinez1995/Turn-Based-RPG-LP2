using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGlib.Characters;

namespace RPGlib.Itens
{
    public class PotionLife : Item
    {
        public int Life { get; set; }

        public PotionLife(int gainedHP)
        {
            
            this.Life = gainedHP;
            Description = "bufa " + this.Life + ".";
        }

        public override void Efeito(Character person)
        {
            if (person.currentHP + this.Life > person.maxHealth)
            {
                person.currentHP = person.maxHealth;
            }
            else
            {
                person.currentHP = person.currentHP + this.Life;
            }
        }
    }
}
