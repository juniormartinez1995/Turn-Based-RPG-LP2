using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGlib.Itens;
using RPGlib.Characters;


namespace RPGlib.Itens
{
   public class CrimsomVanguard : Item
    {
        public  CrimsomVanguard(int armor, int health)
        {

           // this.LocalImage;
            this.itemName = "Crimsom Vanguard";
            this.Armor = armor;
            this.criticalRate = 0;
            this.Damage = 0;
            this.evasionRate = 0;
            this.Health = Health;
            this.Mana = 0;
            this.Description = "HP = 100\nArmor = 10";
        }

        public override void Effect(Character person)
        {
            person.currentArmor += this.Armor;
            person.currentHP += this.Health;

        }
    }
}
