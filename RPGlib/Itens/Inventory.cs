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


            //PENSAR UM POUCO AQUI
            public Boolean Add_Item(Item item_add)
            {
                if (inventoryList.Count() >= 6)
                {
                    return false;
                }
                else
                {
                    inventoryList.Add(item_add);
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

            /*private int searchInventory()
            {
                for(int i=0;i<inventory.Length;i++)
                {
                    if (inventory[i]==null)
                    {
                        return i;
                    }
                }
                return -1;
            }*/

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

            public Boolean AddVerification(Item item)
            {
                Boolean var = AddPotion(item);

                if (!var)
                {
                    return Add_Item(item);
                }
                return true;
            }

        }
    }