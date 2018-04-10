using RPGlib.Itens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using RPGlib;

namespace RPGlib.Characters
{
    abstract public class Character
    {
        public String Name = "Caio";
        public double CurrentPosX { get; set; }
        public double CurrentPosY { get; set; }
        public int CurrentHP { get; set; }
        public int MaxHealth { get; set; }
        public int MaxMana { get; set; }
        public int CurrentMana { get; set; }
        public int CurrentArmor { get; set; }
        public int EvasionRate { get; set; }
        public int CriticRate { get; set; }
        public int CurrentXP { get; set; }
        public int MaxXP { get; set; } = 100;
        public int GainedXP { get; set; }
        public int Level { get; set; }
        public int Damage { get; set; }


        public BitmapImage RightMoviment { get; set; }
        public BitmapImage LeftMoviment { get; set; }
        public BitmapImage UpMoviment { get; set; }
        public BitmapImage DownMoviment { get; set; }
        public BitmapImage IdleRight { get; set; }
        public BitmapImage IdleLeft { get; set; }
        public BitmapImage IdleUp { get; set; }
        public BitmapImage IdleDown { get; set; }

        public Inventory inventory = new Inventory();

        public void OpenChest(Chest chest)
        {
            for (int x = chest.ItemChest.Count() - 1; x >= 0; x--)
            {
                if (inventory.AddVerification(chest.ItemChest.ElementAt(x), this))
                {
                    chest.ItemChest.RemoveAt(x);
                }

            }
            if (chest.ItemChest.Count() == 0) chest.isOpen = true;

        }

        public bool CountCritic()
        {
            int criticCalc = RandomElement.Limiter(0, 100);

            return criticCalc <= CriticRate;

        }

        public int BasicSkill()
        {

            if (CountCritic())
            {
                return 2 * Damage;
            }
            return Damage;
        }


        //public delegate void upLevelHandler(object sender, EventArgs e);
        //public event upLevelHandler upLevel;

        public bool LevelUp(int xpGain)
        {
            while ((this.CurrentXP += xpGain) > MaxXP)
            {
                this.CurrentXP -= MaxXP;
                this.Level += 1;
                MinimunXPlevel();
                return true;

            }
            return false;
        }

        public void MinimunXPlevel()
        {
            MaxXP += (10 * this.Level);

        }

        public bool IsDead()
        {
            return this.CurrentHP <= 0;
        }

        public bool ManaCountDown(int currentSkill)
        {
            if (CurrentMana < currentSkill)
                return false; //NAO PODE USAR A SKILL
            else
            {
                CurrentMana -= currentSkill;
                return true; //CAIO USOU A SKILL
            }
        }
    }
}

//verificar espaco inventario possibilidade de entrar apenas 1 item