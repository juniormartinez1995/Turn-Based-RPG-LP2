using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGlib.Mobs
{
    public abstract class MobFactoryAB
    {
        public abstract Mob CreateMob(String MobType);
    }
}
