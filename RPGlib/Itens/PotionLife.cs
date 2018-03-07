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
        //Description = descricao da acao do item
        //LocalImage = caminho da imagem q representa o item
        public PotionLife(int gainedHP)
        {
            
            this.Life = gainedHP;
            Description = "bufa " + this.Life + ".";

        }
        //person = personagem
        //correntHP = vida atual do personagem
        //maxHearth = vida maxima do personagem

        public override void Effect(Character person)
        {
            if (person.currentHP + this.Life > person.maxHealth)//verifica se a soma da vida atual do personagem
            {// mais a vida fornecida for maior ou igual a mana maxima do personagem, se sim:

                person.currentHP = person.maxHealth; // a vida atual vai ser igual a vida maxima
            }
            else //se nao:
            {
                person.currentHP = person.currentHP + this.Life;// a vida atual do personagem vai ser igual a vida
                //atual mais a vida fornecida
            }
        }
    }
}
