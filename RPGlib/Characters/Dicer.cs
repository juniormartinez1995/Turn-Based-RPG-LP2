using RPGlib.Itens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGlib.Characters
{
    class Dicer : Character
    {

        public Dicer()
        {
            
            this.currentHP = 100;
            this.maxHealth = 100;
            this.maxHealth = 200;
            this.currentMana = 200;
            this.currentXP = 0;
            this.Level = 0;
            this.criticRate = 10;
            this.evasionRate = 5;
            this.currentArmor = 10;
            this.Damage = 15;
            //UpMoviment = new BitmapImage(new Uri(@"ms-appx:///Assets/cimagif.gif"));  //Achar o img do dicer
           // DownMoviment = new BitmapImage(new Uri(@"ms-appx:///Assets/baixogif.gif"));
           // RightMoviment = new BitmapImage(new Uri(@"ms-appx:///Assets/dirgif.gif"));
            //LeftMoviment = new BitmapImage(new Uri(@"ms-appx:///Assets/esqgif.gif"));

            // IdleDown = new BitmapImage(new Uri(@"ms-appx:///Assets/downAnimation/0.png"));
            // IdleUp = new BitmapImage(new Uri(@"ms-appx:///Assets/upAnimation/0.png"));
            // IdleLeft = new BitmapImage(new Uri(@"ms-appx:///Assets/leftAnimation/0.png"));
            // IdleRight = new BitmapImage(new Uri(@"ms-appx:///Assets/rightAnimation/0.png"));
        }


        public int Multicast() //Retorna um inteiro que será o multiplicador de quantas vezes a skill será castada
        {
       
            int multicastchance = RandomElement.Limiter(0, 100);

            if (multicastchance <= 5)
            {
                return 5;
            }

            if(multicastchance<= 10)
            {
                return 4;
            }

            if (multicastchance <= 15)
            {
                return 3;
            }

            if (multicastchance <= 20)
            {
                return 2;
            }

            return 1;
        }

        public override int SkillBasic()
        {
            return 1;
        }

    }
}

    

