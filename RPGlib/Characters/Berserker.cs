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
            this.currentHP = 250;
            this.maxHealth = 250;
            this.maxMana = 100;
            this.currentMana = 100;
            this.currentXP = 0;
            this.Level = 0;
            this.criticRate = 15;
            this.evasionRate = 5;
            this.currentArmor = 20;
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


        public float SacrificeBlood()
        {
            int hp_missing = this.maxHealth - this.currentHP;
            float multiplier = (100 * hp_missing) / this.maxHealth;

            return multiplier;
        }
    
    }
}
