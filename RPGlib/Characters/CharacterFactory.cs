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

            if (CharacterType.Equals("Berserker")) return new Berserker();
            else if (CharacterType.Equals("Dicer")) return new Dicer();
            else return null;
        }
    }
}

