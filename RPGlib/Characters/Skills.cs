using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGlib.Characters
{
    abstract public class Skills
    {
        public abstract int SkillB();
        public abstract int Skill2();
        public abstract int Skill3();

        public int custoMana { get; set; }
        public int damageSkillB { get; set; }
        public int damageSkill2 { get; set; }
        public int damageSkill3 { get; set; }
    }
}
