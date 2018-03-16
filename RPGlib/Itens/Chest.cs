using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace RPGlib.Itens
{
    public class Chest
    {
        public List<Item> ItemChest = new List<Item>();
        public BitmapImage chestImage { get; set; }

        public Boolean isOpen = false;
    }
}
