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
        }

        public int MaxRibbons = 5;

        public override void StartQuest()
        {
            
        }

        public override void EndQuest()
        {
            
        }
        static void CountRibbons()
        {

        }
        public void Quest()
        {

        }
    }
}
