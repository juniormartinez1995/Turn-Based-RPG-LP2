using RPGlib.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGlib.Itens
{
    public class Inventory
    {

        public List<Item> inventoryList = new List<Item>();
        public List<PotionLife> inventoryPotionLife = new List<PotionLife>();
        public List<PotionMana> inventoryPotionMana = new List<PotionMana>();
        public List<Item> ItensMap = new List<Item>();

        //PENSAR UM POUCO AQUI

        public Boolean Add_Item(Item item_current, Character player)
        {
            if (inventoryList.Count() >= 6)
            {
                return false;
            }
            else
            {
                item_current.Effect(player);
                inventoryList.Add(item_current);
                return true;
            }
        }

        public void Remove_Item(Item item, Character person)
        {
            person.CriticRate -= item.CriticalRate;
            person.CurrentArmor -= item.Armor;
            person.CurrentHP -= item.Health;
            person.CurrentMana -= item.Mana;
            person.EvasionRate -= item.EvasionRate;
            //REMOVE DO PERSONAGEM
            inventoryList.Remove(item);

        }

        public Boolean AddPotion(Item Potion)
        {

            if (Potion is PotionLife)
            {
                PotionLife p = Potion as PotionLife;
                inventoryPotionLife.Add(p);
                return true;
            }
            else if (Potion is PotionMana)
            {
                PotionMana p = Potion as PotionMana;
                inventoryPotionMana.Add(p);
                return true;
            }
            return false;
        }

        public void removeOrUsePotion (Item Potion)
        {
            if (Potion is PotionLife )
            {
                PotionLife p = Potion as PotionLife;
                inventoryPotionLife.Remove(p);
                
            }
            else if (Potion is PotionMana )
            {
                PotionMana p = Potion as PotionMana;
                inventoryPotionMana.Remove(p);
                
            }

        }

        public void removeAndUseLifePot (Character player)
        {
            this.inventoryPotionLife[this.inventoryPotionLife.Count-1].Effect(player);
            this.inventoryPotionLife.Remove(this.inventoryPotionLife[this.inventoryPotionLife.Count - 1] as PotionLife);
        }

        public void removeAndUseManaPot (Character player)
        {
            this.inventoryPotionMana[this.inventoryPotionMana.Count - 1].Effect(player);
            this.inventoryPotionMana.Remove(this.inventoryPotionMana[this.inventoryPotionMana.Count - 1] as PotionMana);
        }

        public Boolean checkPotCount (String Potion)
        {
            if (Potion is "LifePot" && this.inventoryPotionLife.Count() > 0) return true;
            else if (Potion is "ManaPot" && this.inventoryPotionMana.Count() > 0) return true;

            return false;
        }

        public Boolean AddVerification(Item item, Character player)
        {
            Boolean var = AddPotion(item);
            Boolean var2 = false;

            if (var == false && item is ItemMap)
            {
                ItensMap.Add(item);
                var2 = true;
                
            }
            if (var == false && var2 == false)
            {

                return Add_Item(item, player);

            }
            return true;
        }

    }
}