using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace RPGlib.Mobs
{
    public class Ninja : Mob
    {
        public Ninja()
        {
            this.name = "Ninja";
            this.HP = 200;
            this.Mana = 50;
            this.evasionRate = 20;
            this.currentArmor = 10;
            this.Damage = 30;
            this.GifBattle = new BitmapImage(new Uri(@"ms-appx:///Assets/newninja.gif")); ;

        }
    }
}
