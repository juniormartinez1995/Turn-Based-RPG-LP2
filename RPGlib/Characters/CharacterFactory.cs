using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGlib.Mobs;

namespace RPGlib.Characters
{
    public class CharacterFactory : CharacterFactoryAB
    {
        public override Character CreateCharacter(String CharacterType)
        {
            switch (CharacterType)
            {
                case "Berserker":
                    return new Berserker();
                case "Dicer":
                    return new Dicer();
            }
            return null;
        }

        public override Mob CreateMob(String MobType)
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

