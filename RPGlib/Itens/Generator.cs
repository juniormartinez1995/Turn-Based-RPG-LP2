using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGlib.Itens
{
    static class Generator
    {

        public static PotionLife GeneratePotionLifeAleatory()
        {
            return new PotionLife(RandomElement.Limiter(10, 50));

        }

        public static PotionMana GeneratePotionManaAleatory()
        {
            return new PotionMana(RandomElement.Limiter(10, 50));
        }

        public static void ChestPopulate(Chest c)
        {
            /*
             * 1 = Pocao de vida
             * 2 = pocao de mana
             * 
             */
            int qnt_item = RandomElement.Limiter(0, 2);
            int typeItem = RandomElement.Limiter(1, 2);

            if (qnt_item == 0)
            {
                c.item1 = new Stone();
                c.item2 = new Stone();
            }
            else if (qnt_item == 1)
            {
                c.item1 = new Stone();

                if (typeItem == 1)
                {
                    c.item2 = GeneratePotionLifeAleatory();
                }
                if (typeItem == 2)
                {
                    c.item2 = GeneratePotionManaAleatory();
                }

            }
            else
            {
                if (typeItem == 1)
                {
                    c.item1 = GeneratePotionLifeAleatory();
                }
                if (typeItem == 2)
                {
                    c.item1 = GeneratePotionManaAleatory();
                }

                int typeItem2 = RandomElement.Limiter(0, 2);

                if (typeItem2 == 1)
                {
                    c.item2 = GeneratePotionLifeAleatory();
                }
                if (typeItem2 == 2)
                {
                    c.item2 = GeneratePotionManaAleatory();
                }
            }

        }

    }
}
