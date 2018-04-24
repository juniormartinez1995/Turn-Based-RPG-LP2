using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace RPGlib.Itens
{
    abstract public class ItemMap : Item
    {
        public ItemMap(){
            Damage = 0;
            Armor = 0;
            Health =0;
            CriticalRate = 0;
            EvasionRate = 0;
            Mana = 0;
            Lifesteal = 0;
            Speed = 0;
        }
    }
}
