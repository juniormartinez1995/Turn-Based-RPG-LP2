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
        public String name = "Caio";
        public double CurrentPosX { get; set; }
        public double CurrentPosY { get; set; }
        public int currentHP { get; set; }
        public int maxHealth { get; set; }
        public int maxMana { get; set; }
        public int currentMana { get; set; }
        public int currentArmor { get; set; }
        public int evasionRate { get; set; }
        public int criticRate { get; set; }
        public int currentXP { get; set; }
        public int maxXP { get; set; } = 100;
        public int gainedXP { get; set; }
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
                if (inventory.AddVerification(chest.ItemChest.ElementAt(x)))
                {
                    chest.ItemChest.RemoveAt(x);
                }

            }
            if (chest.ItemChest.Count() == 0)
                chest.isOpen = true;

        }

        public bool CountCritic()
        {
            int criticCalc = RandomElement.Limiter(0, 100);

            return criticCalc <= criticRate;

        }

        public int SkillBasic()
        {
            if (CountCritic())
            {
                return 2 * Damage;
            }
            return Damage;
           
        }


        //public delegate void upLevelHandler(object sender, EventArgs e);
        //public event upLevelHandler upLevel;

        public bool upLevel(int xpGain)
        {
            while ((this.currentXP += xpGain) > maxXP)
            {
                this.currentXP -= maxXP;
                this.Level += 1;
                minimunXPlevel();
                return true;

            }
            return false;
        }
        
        public void minimunXPlevel()
        {
            maxXP += (10*this.Level);
            
        }

        public bool IsDead()
        {
            if (this.currentHP <= 0) return true;
            else return false;
        }


    }
}

//verificar espaco inventario possibilidade de entrar apenas 1 item