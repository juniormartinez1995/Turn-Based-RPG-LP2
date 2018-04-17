using RPGlib.Itens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace RPGlib.Characters
{
    public class Dicer : Character
    {

        public Dicer()
        {

            this.CurrentHP = 100;
            this.MaxHealth = 100;
            this.MaxMana = 200;
            this.CurrentMana = 200;
            this.CurrentXP = 0;
            this.Level = 0;
            this.CriticRate = 10;
            this.EvasionRate = 5;
            this.CurrentArmor = 10;
            this.Damage = 15;
            this.Lifesteal = 0;

            UpMoviment = new BitmapImage(new Uri(@"ms-appx:///Assets/rightAnimation/dr.gif"));  //Achar o img do dicer
            DownMoviment = new BitmapImage(new Uri(@"ms-appx:///Assets/leftAnimation/dl.gif"));
            RightMoviment = new BitmapImage(new Uri(@"ms-appx:///Assets/rightAnimation/dr.gif"));
            LeftMoviment = new BitmapImage(new Uri(@"ms-appx:///Assets/leftAnimation/dl.gif"));

            IdleDown = new BitmapImage(new Uri(@"ms-appx:///Assets/leftAnimation/dls.png"));
            IdleUp = new BitmapImage(new Uri(@"ms-appx:///Assets/rightAnimation/drs.png"));
            IdleLeft = new BitmapImage(new Uri(@"ms-appx:///Assets/leftAnimation/dls.png"));
            IdleRight = new BitmapImage(new Uri(@"ms-appx:///Assets/rightAnimation/drs.png"));
        }


        public int Multicast() //Retorna um inteiro que será o multiplicador de quantas vezes a skill será castada
        {

            int multicastchance = RandomElement.Limiter(0, 100);

            if (multicastchance <=  10)
            {
                return 5;
            }

            if (multicastchance <= 20)
            {
                return 4;
            }

            if (multicastchance <= 30)
            {
                return 3;
            }

            if (multicastchance <= 40)
            {
                return 2;
            }

            return 1;
        }


    }
}



