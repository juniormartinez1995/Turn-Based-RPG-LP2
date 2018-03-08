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
            this.name = name;
            this.currentHP = 250;
            this.currentMana = 100;
            this.currentXP = 0;
            this.Level = 0;
            this.criticRate = 15;
            this.evasionRate = 5;
            this.currentArmor = 20;
            this.Damage = 30;
            UpMoviment = new BitmapImage(new Uri(@"ms-appx:///Assets/cimagif.gif"));
            DownMoviment = new BitmapImage(new Uri(@"ms-appx:///Assets/baixogif.gif"));
            RightMoviment = new BitmapImage(new Uri(@"ms-appx:///Assets/dirgif.gif"));
            LeftMoviment = new BitmapImage(new Uri(@"ms-appx:///Assets/esqgif.gif"));

            IdleDown = new BitmapImage(new Uri(@"ms-appx:///Assets/downAnimation/0.png"));
            IdleUp = new BitmapImage(new Uri(@"ms-appx:///Assets/upAnimation/0.png"));
            IdleLeft = new BitmapImage(new Uri(@"ms-appx:///Assets/leftAnimation/0.png"));
            IdleRight = new BitmapImage(new Uri(@"ms-appx:///Assets/rightAnimation/0.png"));
        }

        public bool upLevel()
        {
            if ((this.currentXP + this.gainedXP) > 100)
            {
                this.currentXP = 0;
                this.Level += 1;
                return true;

            }
            return false;
        }

        public bool IsDead()
        {
            if (this.currentHP <= 0) return true;
            else return false;
        }

        public bool CountCritic()
        {

            Random random = new Random();
            int criticCalc = random.Next(0, 100);
            if (criticCalc <= this.criticRate)
            {
                return true;
            }
            else return false;
        }

    }
}
