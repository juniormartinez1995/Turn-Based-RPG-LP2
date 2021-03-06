﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGlib.Itens
{
    public static class Generator
    {
        public static void ChestPopulate(Chest c)
        {

            /**
             * 1 - Stone
             * 2 - PotionLife
             * 3 - PotionMana
             * 4 - CrimsonVanguard
             * 5 - DemonicRapier
             * 6 - RabbitFeet
             **/
             
            int numRandom = RandomElement.Limiter(1, 6);
            numRandom = 2;
            switch (numRandom)
            {
                case 1:
                    c.ItemChest.Add(new Stone(2));
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
                case 5:
                    c.ItemChest.Add(new DemonicRapier(RandomElement.Limiter(20, 30)));
                    break;
                case 6:
                    c.ItemChest.Add(new RabbitFeet(RandomElement.Limiter(8, 10)));
                    break;
                case 7:
                    c.ItemChest.Add(new HermesBoots(2));
                    break;
            }

            int numRandom2 = RandomElement.Limiter(1, 6);
            switch (numRandom2)
            {
                case 1:
                    c.ItemChest.Add(new Stone(2));
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
                case 5:
                    c.ItemChest.Add(new DemonicRapier(RandomElement.Limiter(20, 30)));
                    break;
                case 6:
                    c.ItemChest.Add(new RabbitFeet(RandomElement.Limiter(8, 10)));
                    break;
                case 7:
                    c.ItemChest.Add(new DraculaTeeth(RandomElement.Limiter(50, 100)));
                    break;
                case 8:
                    c.ItemChest.Add(new HermesBoots(2));
                    break;
            }
        }

    }
}