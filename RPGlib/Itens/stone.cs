using RPGlib.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGlib.Itens
{
    public class Stone : Item
    {
        public Stone()
        {
            this.Health = 0;
            this.itemName = "Pedra";
            this.Description = "Tem uma pedra no seu caminho.";
            this.LocalImage = "";

            //---------------------------------------------

            this.Armor = 0;
            this.criticalRate = 0;
            this.evasionRate = 0;
            this.Damage = 0;
            this.Mana = 0;
            //VELOCIDADE DO PERSONAGEM
        }

        public override void Effect(Character person) { }


    }
}
