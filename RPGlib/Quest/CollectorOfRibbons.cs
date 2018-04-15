using RPGlib.Characters;
using RPGlib.Itens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGlib.Quest
{
    public class CollectorOfRibbons : Quest
    {
        public CollectorOfRibbons()
        {
            this.name = "Colecionador de Lacos";

            this.description = "Caio é um colecionador de Lacos," +
                " nao consegue ver um laco que o pega, mas ocorreu algo terrivel" +
                "alguns de seus lacos sumiram.";

            this.objective = "Ajude caio a recuperar os " + MaxRibbons + " lacos rosas" +
                "perdido.";
            this.reward = "Recompensa: "+ XP_Reward;
        }
        public int XP_Reward = 50;
        public int MaxRibbons = 5;

        public override void StartQuest()
        {
            
        }

        public override void EndQuest()
        {
            /*
             * acho q precisa fazer um evento q sempre q terminar uma missao 
             * ele excluir do inventoryQuest, pra n ter q fica percorrendo a lista 
             * e verficando se terminou ou nao
             * 
             */
        }

        public int CountRibbons(Character Person)
        {
            int count = 0;

            foreach (Item item in Person.inventory.inventoryList)
            {
                if(item is Ribbon)
                {
                    count++;
                }
            }
            return count;
            
        }

        public void Quest()
        {

        }
    }
}
