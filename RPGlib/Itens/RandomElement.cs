using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGlib.Itens
{
    public static class RandomElement
    {
        private static Random Aleatory = new Random();

        public static int Limiter(int MinimumValue, int MaximumValue)
        {
            return Aleatory.Next(MinimumValue, MaximumValue);
        }
    }
}



