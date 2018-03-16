using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGlib.Itens
{
    public static class Generator
    {

        /*public static PotionLife GeneratePotionLifeAleatory()
        {
            return new PotionLife(RandomElement.Limiter(10, 50));

        }

        public static PotionMana GeneratePotionManaAleatory()
        {
            return new PotionMana(RandomElement.Limiter(10, 50));
        }*/

        public static void ChestPopulate(Chest c)
        {
            /*
             * 1 = Pocao de vida
             * 2 = pocao de mana
             * 3 = CrisomVanguard
             */
            /*int qnt_item = RandomElement.Limiter(0, 2);
            int typeItem = RandomElement.Limiter(1, 3);

            if (qnt_item == 0)
            {
                c.ItemChest.Add(new Stone());
                c.ItemChest.Add(new Stone());
            }
            else if (qnt_item == 1)
            {
                c.ItemChest.Add(new Stone());

                if (typeItem == 1)
                {
                    c.ItemChest.Add(GeneratePotionLifeAleatory());
                }
                if (typeItem == 2)
                {
                    c.ItemChest.Add(GeneratePotionManaAleatory());
                }
                if(typeItem == 3)
                {
                    c.ItemChest.Add(new CrimsomVanguard(RandomElement.Limiter(5, 10), RandomElement.Limiter(50, 100)));
                }

            }
            else
            {
                if (typeItem == 1)
                {
                    c.ItemChest.Add(GeneratePotionLifeAleatory());
                }
                if (typeItem == 2)
                {
                    c.ItemChest.Add(GeneratePotionManaAleatory());
                }
                if (typeItem == 3)
                {
                    c.ItemChest.Add(new CrimsomVanguard(RandomElement.Limiter(5, 10), RandomElement.Limiter(50, 100)));
                }

                int typeItem2 = RandomElement.Limiter(1, 3);

                if (typeItem2 == 1)
                {
                    c.ItemChest.Add(GeneratePotionLifeAleatory());
                }
                if (typeItem2 == 2)
                {
                    c.ItemChest.Add(GeneratePotionManaAleatory());
                }
                if (typeItem2 == 3)
                {
                    c.ItemChest.Add(new CrimsomVanguard(RandomElement.Limiter(5, 10), RandomElement.Limiter(50, 100)));
                }
            }*/
            int numRandom = RandomElement.Limiter(1,4);

            switch (numRandom)
            {
                case 1:
                    c.ItemChest.Add(new Stone());
                    break;
                case 2:
                    c.ItemChest.Add(new PotionLife());
                    break;
                case 3:
                    c.ItemChest.Add(new PotionMana());
                    break;
                case 4:
                    c.ItemChest.Add(new CrimsomVanguard(RandomElement.Limiter(5, 10), RandomElement.Limiter(50, 100)));
                    break;
            }
            int numRandom2 = RandomElement.Limiter(1, 4);

            switch (numRandom2)
            {
                case 1:
                    c.ItemChest.Add(new Stone());
                    break;
                case 2:
                    c.ItemChest.Add(new PotionLife());
                    break;
                case 3:
                    c.ItemChest.Add(new PotionMana());
                    break;
                case 4:
                    c.ItemChest.Add(new CrimsomVanguard(RandomElement.Limiter(5, 10), RandomElement.Limiter(50, 100)));
                    break;
            }
        }

    }
}
