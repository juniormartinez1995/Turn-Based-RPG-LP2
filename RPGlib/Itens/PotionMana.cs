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

        public PotionMana(int qntMana)
        {
            this.Mana = qntMana;
            Description = "";
            CaminhoImagem = "";
        }

        /*public override void Efeito(Character personagem)
        {
            if (personagem.manaAtual + this.Mana >= personagem.manaMax) //verifica de a soma da mana atual do personagem
            {// mais a mana fornecida for maior ou igual a mana maxima do personagem, se sim:

                personagem.manaAtual = personagem.manaMax;// a mana atual va ser igual a mana maxima
            }
            else//se nao:
            {
                personagem.manaAtual = personagem.manaAtual + this.Mana; // a mana atual do personagem vai ser igual a mana
                //atual mais a mana fornecida
            }
        }*/
    }
}