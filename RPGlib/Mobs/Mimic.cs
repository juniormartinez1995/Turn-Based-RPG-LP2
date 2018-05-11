using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace RPGlib.Mobs
{
    public class Mimic : Boss
    {
        public Mimic()
        {
            this.name = "Mimic";
            this.HP = 300;
            //Pq mob ta com mana
            //this.Mana = 100;
            this.evasionRate = 14;
            this.currentArmor = 5;
            this.Damage = 50;
            this.GifBattle = new BitmapImage(new Uri(@"ms-appx:///Assets/BattleAnimations/mimic.gif"));
            this.castBattle = new BitmapImage(new Uri(@"ms-appx:///Assets/Book.gif"));
        }

        public override void Skill_Normal()
        {

        }

        public override void Skill_Ultimate()
        {

        }


    }
}
