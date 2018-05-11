using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace RPGlib.Mobs
{
    public class Mouse : Mob
    {
        public Mouse()
        {
            this.name = "Mouse";
            this.HP = 100;
            this.evasionRate = 20;
            this.currentArmor = 0;
            this.Damage = 5;
            this.GifBattle = new BitmapImage(new Uri(@"ms-appx:///Assets/mouse.gif")); 
            this.castBattle = new BitmapImage(new Uri(@"ms-appx:///Assets/cheese.png")); 
        }
    }
}
