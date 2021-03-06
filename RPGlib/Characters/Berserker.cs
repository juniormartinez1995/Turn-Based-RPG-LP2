﻿using RPGlib.Itens;
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
            this.Lifesteal = 5;

            Attacking = new BitmapImage(new Uri(@"ms-appx:///Assets/AttackGifs/batk.gif"));
            Dying = new BitmapImage(new Uri(@"ms-appx:///Assets/DyingGifs/b_dy.gif"));

            UpMoviment = new BitmapImage(new Uri(@"ms-appx:///Assets/AnimaçãoCharacters/br.gif"));
            DownMoviment = new BitmapImage(new Uri(@"ms-appx:///Assets/AnimaçãoCharacters/bl.gif"));
            RightMoviment = new BitmapImage(new Uri(@"ms-appx:///Assets/AnimaçãoCharacters/br.gif"));
            LeftMoviment = new BitmapImage(new Uri(@"ms-appx:///Assets/AnimaçãoCharacters/bl.gif"));

            IdleDown = new BitmapImage(new Uri(@"ms-appx:///Assets/AnimaçãoCharacters/bls.gif"));
            IdleUp = new BitmapImage(new Uri(@"ms-appx:///Assets/AnimaçãoCharacters/brs.gif"));
            IdleLeft = new BitmapImage(new Uri(@"ms-appx:///Assets/AnimaçãoCharacters/bls.gif"));
            IdleRight = new BitmapImage(new Uri(@"ms-appx:///Assets/AnimaçãoCharacters/brs.gif"));

            FirstSkill = new BitmapImage(new Uri(@"ms-appx:///Assets/SwordFinal.png"));
            SecondSkill = new BitmapImage(new Uri(@"ms-appx:///Assets/Sword2New2.gif"));
            ThirdSkill = new BitmapImage(new Uri(@"ms-appx:///Assets/3rdBerserker.gif"));
        }


        public override int Passive() //Está balanceada!
        {

            float missing_hp = this.MaxHealth - this.CurrentHP;

            if (missing_hp == 0)  //Caso a vida estiver cheia
            {
                return 1;
            }

            float multiplier = missing_hp / this.MaxHealth;

            if (multiplier >= 0.0001 && multiplier <= 0.3) //Entre 0.1% e 30% de hp faltante, o dano é multiplicado por 1
            {
                return 1;
            }
            else if (multiplier >= 0.3 && multiplier <= 0.6)  //Entre 30% e 60% de hp faltante, o dano é multiplicado por 2
            {

                return 2;
            }
            else if (multiplier >= 0.6 && multiplier <= 0.9)
            {  //Entre 40% e 90% de hp faltante, o dano é multiplicado por 1

                return 3;
            }

            return 4; //Acima de 90% de hp faltante, o dano é multiplicado por 4

        }
        public override int BasicSkill()
        {
            int cura;
            int lifestealdmg;
            if (Lifesteal == 0) //Caso não haja lifesteal
            {

                if (CountCritic())
                {
                    return 2 * Damage * (int)Passive();
                }
                return Damage * (int)Passive();

            }
            else
            {  //Caso haja lifesteal

                if (CountCritic()) //Caso o ataque seja crítico
                {
                    lifestealdmg = 2 * Damage * (int)Passive();
                    cura = (int)((float)lifestealdmg * ((float)this.Lifesteal / 100)); //Dano que vai ser transformado em cura por %
                    if (CurrentHP + cura > MaxHealth) //Se a vida atual + a cura der maior que a vida máxima, o personagem ficará com a vida máxima
                    {
                        this.CurrentHP = MaxHealth;
                        return lifestealdmg;
                    }
                    else
                    {
                        this.CurrentHP += cura;
                        return lifestealdmg;
                    }
                }
                //Caso o ataque não seja crítico
                lifestealdmg = Damage * (int)Passive();
                cura = (int)((float)lifestealdmg * ((float)this.Lifesteal / 100)); //Dano que vai ser transformado em cura por %
                if (CurrentHP + cura > MaxHealth)//Se a vida atual + a cura der maior que a vida máxima, o personagem ficará com a vida máxima
                {
                    this.CurrentHP = MaxHealth;
                    return lifestealdmg;
                }
                else
                {
                    this.CurrentHP += cura;
                    return lifestealdmg;
                }


            }


        }

        public override int Skill1()
        {
           
            return 80;

        }

        public override int Skill2()
        {
            int lifecost = this.CurrentHP / 2;
            return lifecost;
        }
    }
}
