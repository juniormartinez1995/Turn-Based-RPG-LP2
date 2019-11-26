using RPGlib.Mobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGlib.Characters
{
    public abstract class CharacterFactoryAB
    {
        public abstract Character CreateCharacter(String CharacterType);

        public abstract Mob CreateMob(String MobType);
    }
}
