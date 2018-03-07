using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGlib.Itens
{
    public class Inventory
    {

        Item[] inventory = new Item[6];
        List<PotionLife> inventoryPotionLife = new List<PotionLife>();
        List<PotionMana> iventoryPotionMana = new List<PotionMana>();

        private string Add_Item(Item item_add)
        {
            if(searchInventory() == -1)
            {
                return "Iventario cheio";
            }
            else
            {
                inventory[searchInventory()] = item_add;
                return "Item adicionado";
            }
        }

        private void Remover_Item(int id)
        {
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
        
        private void removeOrUsePotion(Item Potion)
        {
            if (Potion.GetType() == typeof(PotionLife))
            {
                PotionLife p = Potion as PotionLife;
                inventoryPotionLife.Remove(p);
            }
            else if (Potion.GetType() == typeof(PotionMana))
            {
                PotionMana p = Potion as PotionMana;
                iventoryPotionMana.Remove(p);
            }
            else
            {

            }
            
        }
    }
}
