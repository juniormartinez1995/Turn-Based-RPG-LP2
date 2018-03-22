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
            this.HP = 100;
            this.Mana = 100;
            this.criticRate = 15;
            this.evasionRate = 5;
            this.currentArmor = 20;
            this.Damage = 30;
            this.GifBattle = new BitmapImage(new Uri(@"ms-appx:///Assets/Jynx.gif"));
        }

        public override void Skill_Normal()
        {
            
        }

        public override void Skill_Ultimate()
        {
            
        }

    }
}
