﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGlib.Mobs
{
    public class HardStrategy : IFightStrategy
    {
        public int Fight(Mob M)
        {
           return M.Skill3();
        }
    }
}
