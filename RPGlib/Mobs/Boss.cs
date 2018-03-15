using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGlib.Mobs
{
    abstract public class Boss : Mob
    {
        abstract public void Skill_Normal();
        abstract public void Skill_Ultimate();
    }
}
