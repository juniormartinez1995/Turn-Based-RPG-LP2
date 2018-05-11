using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace RPGlib.Mobs
{
    public class PablloVittar : Boss
    {

        public PablloVittar()
        {
            this.name = "Pabllo Vittar";
            this.HP = 250;
            this.Mana = 100;
            this.evasionRate = 6;
            this.currentArmor = 20;
            this.Damage = 30;
            this.GifBattle = new BitmapImage(new Uri(@"ms-appx:///Assets/Jynx3.gif"));
            this.castBattle = new BitmapImage(new Uri(@"ms-appx:///Assets/jynxheart.gif")); 
        }

        public override void Skill_Normal()
        {
            
        }

        public override void Skill_Ultimate()
        {
            
        }

    }
}
