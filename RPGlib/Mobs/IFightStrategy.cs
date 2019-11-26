using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGlib.Mobs
{
    public interface IFightStrategy
    {
        int Fight(Mob M);
    }
}
