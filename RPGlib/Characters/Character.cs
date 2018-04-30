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
        public int Speed { get; set; } = 3;
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
        public int Lifesteal { get; set; }

        public BitmapImage Attacking { get; set; }
        public BitmapImage Dying { get; set; }
        public BitmapImage RightMoviment { get; set; }
        public BitmapImage LeftMoviment { get; set; }
        public BitmapImage UpMoviment { get; set; }
        public BitmapImage DownMoviment { get; set; }
        public BitmapImage IdleRight { get; set; }
        public BitmapImage IdleLeft { get; set; }
        public BitmapImage IdleUp { get; set; }
        public BitmapImage IdleDown { get; set; }

        public Inventory inventory = new Inventory();

        public delegate void CharacterDeadEventHandler(object sender, EventArgs args);
        public event CharacterDeadEventHandler CharacterDead;

        public delegate void NoManaEventHandler(object sender, EventArgs args);
        public event NoManaEventHandler NoMana;

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

        public virtual int Passive()
        {
            return 1;
        }

        public virtual int BasicSkill()
        {

            if (CountCritic())
            {
                return 2 * Damage;
            }
            return Damage;
        }

        public virtual int Skill1()
        {
            
            return 1;
        }

        public virtual int Skill2()
        {
            return 1;
        }

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

        protected virtual void OnCharacterDead()
        {
            CharacterDead?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnNoMana()
        {
            NoMana?.Invoke(this, EventArgs.Empty);
        }

        public bool IsDead()
        {
            if (this.CurrentHP <= 0) 
            {
                OnCharacterDead();
                return true;
            }
            else return false;
            
        }

        public bool ManaCountDown(int currentSkill)
        {
            if (CurrentMana < currentSkill) 
            { 
                OnNoMana(); //NAO PODE USAR A SKILL
                return false;
            }      
            else
            {
                CurrentMana -= currentSkill;
                return true; //CAIO USOU A SKILL
            }
        }
    }
}

//verificar espaco inventario possibilidade de entrar apenas 1 item