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

        public Item[] inventory = new Item[6];
        public List<PotionLife> inventoryPotionLife = new List<PotionLife>();
        public List<PotionMana> inventoryPotionMana = new List<PotionMana>();


        //PENSAR UM POUCO AQUI
        public string Add_Item(Item item_add)
        {
            if(searchInventory() == -1)
            {
                return "Inventario cheio";
            }
            else
            {
                inventory[searchInventory()] = item_add;
                return "Item adicionado";
            }
        }

        public void Remover_Item(int id, Character person)
        {
            person.criticRate -= inventory[id].criticalRate;
            person.currentArmor -= inventory[id].Armor;
            person.currentHP -= inventory[id].Health;
            person.currentMana -= inventory[id].Mana;
            person.evasionRate -= inventory[id].evasionRate;
            //VELOCIDADE DO PERSONAGEM
            inventory[id] = null;

        }

        private int searchInventory()
        {
            for(int i=0;i<inventory.Length;i++)
            {
                if (inventory[i]==null)
                {
                    return i;
                }
            }
            return -1;
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

        public void removeOrUsePotion(Item Potion)
        {
            if (Potion is PotionLife)
            {
                PotionLife p = Potion as PotionLife;
                inventoryPotionLife.Remove(p);
            }
            else if (Potion is PotionMana)
            {
                PotionMana p = Potion as PotionMana;
                inventoryPotionMana.Remove(p);
            }
            
        }

        public void AddVerification(Item item)
        {
            Boolean var = AddPotion(item);

            if (!var)
            {
                Add_Item(item);
            }
        }
        
    }
}
