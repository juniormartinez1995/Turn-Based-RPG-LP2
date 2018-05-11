using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace RPGlib.Mobs
{
    public class Salamander : Mob
    {
        public Salamander()
        {
            this.name = "Salamander";
            this.HP = 150;
            this.evasionRate = 20;
            this.currentArmor = 0;
            this.Damage = 15;
            this.GifBattle = new BitmapImage(new Uri(@"ms-appx:///Assets/charizard.jpg")); 
            this.castBattle = new BitmapImage(new Uri(@"ms-appx:///Assets/salamanderfire.gif")); 

        }
    }
}
