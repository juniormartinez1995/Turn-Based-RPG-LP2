using RPGlib.Characters;
using RPGlib.Itens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace RPGlib.Quest
{
    public class Ribbon : Item
    {
        public Ribbon()
        {
            this.ItemName = "Laco Rosa";
            this.Description = "Laco de Caio.";
            this.ImageItem = new BitmapImage(new Uri(@"ms-appx:///Assets/Gif_Image_Item_Quest/rosinha.png"));

            //---------------------------------------------

            this.Health = 0;
            this.Armor = 0;
            this.CriticalRate = 0;
            this.EvasionRate = 0;
            this.Damage = 0;
            this.Mana = 0;
            this.Lifesteal = 0;
        }

        public override void Effect(Character person) { }
    }
}
