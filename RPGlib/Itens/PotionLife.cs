﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGlib.Characters;
using RPGlib.Itens;
namespace RPGlib.Itens
{
    public class PotionLife : Item
    {
        //Description = descricao da acao do item
        //LocalImage = caminho da imagem q representa o item
        public PotionLife(int gainedHP)
        {
            this.Health = gainedHP;
            this.itemName = "Poção de Vida";
            this.Description = "bufa " + this.Health + ".";
            this.LocalImage = "";

            //---------------------------------------------

            this.Armor = 0;
            this.criticalRate = 0;
            this.evasionRate = 0;
            this.Damage = 0;
            this.Mana = 0;
            
            

        }
        //person = personagem
        //correntHP = vida atual do personagem
        //maxHearth = vida maxima do personagem

        public override void Effect(Character person)
        {
            if (person.currentHP + Health > person.maxHealth)//verifica se a soma da vida atual do personagem
            {// mais a vida fornecida for maior ou igual a mana maxima do personagem, se sim:

                person.currentHP = person.maxHealth; // a vida atual vai ser igual a vida maxima
            }
            else //se nao:
            {
                person.currentHP = person.currentHP + Health;// a vida atual do personagem vai ser igual a vida
                //atual mais a vida fornecida
            }
        }
    }
}
