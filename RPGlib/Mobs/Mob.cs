using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace RPGlib.Mobs
{
    abstract public class Mob
    {
        public String name { get; set; }
        public int HP { get; set; }
        public int Mana { get; set; }
        public int currentArmor { get; set; }
        public int evasionRate { get; set; }
        public int criticRate { get; set; }
        public int Damage { get; set; }
        public BitmapImage GifBattle { get; set; }
        public BitmapImage RightMoviment { get; set; }
        public BitmapImage LeftMoviment { get; set; }
        public BitmapImage UpMoviment { get; set; }
        public BitmapImage DownMoviment { get; set; }

        abstract public void Skill_Basic();
    }
}
