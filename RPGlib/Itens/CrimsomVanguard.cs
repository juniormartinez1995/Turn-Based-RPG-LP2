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
        public  CrimsomVanguard()
        {

           // this.LocalImage;
            this.itemName = "Crimsom Vanguard";
            this.Armor = 10;
            this.criticalRate = 0;
            this.Damage = 0;
            this.evasionRate = 0;
            this.Health = 100;
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
