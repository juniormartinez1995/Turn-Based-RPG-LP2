using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGlib.Itens;
using RPGlib.Characters;
using Windows.UI.Xaml.Media.Imaging;

namespace RPGlib.Itens
{
   public class CrimsomVanguard : Item
    {
        public  CrimsomVanguard(int armor, int health)
        {
            this.ImageItem = new BitmapImage(new Uri(@"ms-appx:///Assets/crimson_vanguard.png")); 
            this.ItemName = "Crimsom Vanguard";
            this.Armor = armor;
            this.CriticalRate = 0;
            this.Damage = 0;
            this.EvasionRate = 0;
            this.Health = health;
            this.Mana = 0;
            this.Lifesteal = 0;
            this.Description = "HP + " + health +"\nArmor + " + armor;
            this.Speed = 0;
        }

        public override void Effect(Character person)
        {
            person.CurrentArmor += this.Armor;
            person.MaxHealth += this.Health;
        }
    }
}
