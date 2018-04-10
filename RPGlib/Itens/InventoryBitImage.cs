using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace RPGlib.Itens
{
    public class InventoryBitImage
    {
        
        public Image ImageMap { get; set; }
        public BitmapImage BitImage { get; set; }
        
        public InventoryBitImage(Image image, BitmapImage bitmapimage)
        {
            this.ImageMap = image;
            this.BitImage = bitmapimage;
        }

    }
}
