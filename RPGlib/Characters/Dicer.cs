﻿using System;
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
            this.name = name;
            this.currentHP = 100;
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
          //  IdleRight = new BitmapImage(new Uri(@"ms-appx:///Assets/rightAnimation/0.png"));
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

        public int Multicast() //Retorna um inteiro que será o multiplicador de quantas vezes a skill será castada
        {
            Random multi = new Random();
            int multicastchance = multi.Next(0, 10);
            if(multicastchance <= 5)
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
    }
}

    

