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
            if (person.vidaAtual + this.Life > person.maxHealth)
            {
                person.vidaAtual = person.maxHealth;
            }
            else
            {
                person.vidaAtual = person.vidaAtual + this.Life;
            }
        }
    }
}
