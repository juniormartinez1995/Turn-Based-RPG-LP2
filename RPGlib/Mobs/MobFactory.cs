using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGlib.Mobs
{
    public class MobFactory : MobFactoryAB
    {
        public override Mob CreateMob(string MobType)
        {
            switch (MobType)
            {
                case "Mimic":
                    return new Mimic();
                case "Mouse":
                    return new Mouse();
                case "Ninja":
                    return new Ninja();
                case "PablloVittar":
                    return new PablloVittar();
                case "Salamander":
                    return new Salamander();
            }

            return null;
        }
    }
}
