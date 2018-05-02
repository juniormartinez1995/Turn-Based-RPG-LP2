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

            this.CurrentHP = 150;
            this.MaxHealth = 150;
            this.MaxMana = 300;
            this.CurrentMana = 300;
            this.CurrentXP = 0;
            this.Level = 0;
            this.CriticRate = 10;
            this.EvasionRate = 0;
            this.CurrentArmor = 10;
            this.Damage = 50;
            this.Lifesteal = 0;

            Attacking = new BitmapImage(new Uri(@"ms-appx:///Assets/AttackGifs/datk.gif"));
            Dying = new BitmapImage(new Uri(@"ms-appx:///Assets/DyingGifs/d_dy.gif"));

            UpMoviment = new BitmapImage(new Uri(@"ms-appx:///Assets/rightAnimation/dr.gif")); 
            DownMoviment = new BitmapImage(new Uri(@"ms-appx:///Assets/leftAnimation/dl.gif"));
            RightMoviment = new BitmapImage(new Uri(@"ms-appx:///Assets/rightAnimation/dr.gif"));
            LeftMoviment = new BitmapImage(new Uri(@"ms-appx:///Assets/leftAnimation/dl.gif"));

            IdleDown = new BitmapImage(new Uri(@"ms-appx:///Assets/AnimaçãoCharacters/dls.gif"));
            IdleUp = new BitmapImage(new Uri(@"ms-appx:///Assets/AnimaçãoCharacters/drs.gif"));
            IdleLeft = new BitmapImage(new Uri(@"ms-appx:///Assets/AnimaçãoCharacters/dls.gif"));
            IdleRight = new BitmapImage(new Uri(@"ms-appx:///Assets/AnimaçãoCharacters/drs.gif"));

            FirstSkill = new BitmapImage(new Uri(@"ms-appx:///Assets/AttackGifs/Dicer/water_spellq.gif"));
            SecondSkill = new BitmapImage(new Uri(@"ms-appx:///Assets/AttackGifs/Dicer/SnakeDicer.gif"));
            ThirdSkill = new BitmapImage(new Uri(@"ms-appx:///Assets/AttackGifs/Dicer/ghost_spellq.gif"));
        }


        public override int Passive() //Retorna um inteiro que será o multiplicador de quantas vezes a skill será castada
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

        public override int BasicSkill()
        {
            int damageturn = this.Damage * Passive();
            return damageturn;
        }

        //Skill a ser pensada
        public override int Skill1()
        {
            int damageTurn = 30 * Passive();

            return damageTurn;
        }

        //SKill consome 20 de vida para ser castada
        public override int Skill2()
        {
            int damageTurn = 80 * Passive();
            this.CurrentHP -= 20;

            return damageTurn;
        }
    }
}



