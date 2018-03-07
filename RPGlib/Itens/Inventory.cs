using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGlib.Itens
{
    public class Inventory
    {
        //SE LIGUE NESSA POHAAAA
        List<Item> inventory = new List<Item>();
        

        public void Add_Item(Item item_add)
        {
            inventory.Add(item_add);
        }

        public void Remover_Item(Item item_removed)
        {
            inventory.Remove(item_removed);
        }
    }
}
