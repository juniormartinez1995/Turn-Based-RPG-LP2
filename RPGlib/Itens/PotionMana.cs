using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGlib.Characters;

namespace RPGlib.Itens
{
    public class PotionMana : Item
    {
        public int Mana { get; set; }

        //Description = descricao da acao do item
        //LocalImage = caminho da imagem q representa o item

        public PotionMana(int qntMana)
        {
            this.Mana = qntMana;
            Description = "";
            LocalImage = "";
        }
        //person = personagem
        //correnMana = mana atual do personagem
        //maxMana = mana maxima do personagem
        public override void Effect(Character person)
        {
            if (person.currentMana + this.Mana >= person.maxMana) //verifica se a soma da mana atual do personagem
            {// mais a mana fornecida for maior ou igual a mana maxima do personagem, se sim:

                person.currentMana = person.maxMana;// a mana atual va ser igual a mana maxima
            }
            else//se nao:
            {
                person.currentMana = person.currentMana + this.Mana; // a mana atual do personagem vai ser igual a mana
                //atual mais a mana fornecida
            }
        }
    }
}