using RPGlib.Itens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace RPGlib.Characters
{
    public class Berserker : Character
    {

        public Berserker()
        {
            this.CurrentHP = 250;
            this.MaxHealth = 250;
            this.MaxMana = 100;
            this.CurrentMana = 100;
            this.CurrentXP = 0;
            this.Level = 0;
            this.CriticRate = 15;
            this.EvasionRate = 5;
            this.CurrentArmor = 20;
            this.Damage = 30;

            UpMoviment = new BitmapImage(new Uri(@"ms-appx:///Assets/upAnimation/cimagif.gif"));
            DownMoviment = new BitmapImage(new Uri(@"ms-appx:///Assets/downAnimation/baixogif.gif"));
            RightMoviment = new BitmapImage(new Uri(@"ms-appx:///Assets/rightAnimation/dirgif.gif"));
            LeftMoviment = new BitmapImage(new Uri(@"ms-appx:///Assets/leftAnimation/esqgif.gif"));

            IdleDown = new BitmapImage(new Uri(@"ms-appx:///Assets/downAnimation/0.png"));
            IdleUp = new BitmapImage(new Uri(@"ms-appx:///Assets/upAnimation/0.png"));
            IdleLeft = new BitmapImage(new Uri(@"ms-appx:///Assets/leftAnimation/0.png"));
            IdleRight = new BitmapImage(new Uri(@"ms-appx:///Assets/rightAnimation/0.png"));
        }


        public int SacrificeBlood()
        {

            int missing_hp = this.MaxHealth - this.CurrentHP;

            if (missing_hp == 0)  //Caso a vida estiver cheia
            {
                return 1;
            }

            int multiplier = (10 * missing_hp) / this.MaxHealth;

            if (multiplier > 3) {
                return 3;
            }
            if(multiplier <= 1)
            {
                return 1;
            }

            return multiplier;
        }

        public override int BasicSkill()
        {
            if (CountCritic())
            {
                return 2 * Damage * SacrificeBlood();
            }
            return Damage * SacrificeBlood();
            
        }

        
    }
}
