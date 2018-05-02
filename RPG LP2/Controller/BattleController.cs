using RPGlib.Characters;
using RPGlib.Mobs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using RPGlib;
using RPG_LP2;
using System.Threading;


namespace RPG_LP2
{
    public static class BattleController
    {
        // public static Page TelaAtual { get; set; }

        static int Turn = 0;
        static int dmg = 0;
        static int dmgMob = 0;

        public static int InicializeBattle(Character person, Mob mob, int button)
        {

            return Turn;
        }
        static DispatcherTimer timerPlayer = new DispatcherTimer();

        public async static void CheckTurn(Character person, Mob mob, int button) // checa se é o turno do mob ou do player
        {
            if (FinishBattle(person, mob))
            {
                Turn++;
                PlayerTurn(person, mob, button);

            }

            if (FinishBattle(person, mob))
            {
                Turn++;

                await Task.Delay(3000);
                TurnMobAnimation();
                MobTurn(person, mob, button);

            }

        }

        public static int PlayerTurn(Character person, Mob mob, int button)
        {
            //MOSTRAR O NÚMERO DO TURNO
            Debug.WriteLine("Turno: " + Turn);


            switch (button) {
                case 1:
                    if (person is Berserker) {

                        dmg = CheckArmorDamage(person.BasicSkill() - mob.currentArmor);
                        DealMobDamage(dmg, mob);
                    }
                    if (person is Dicer) {

                        if (person.ManaCountDown(50)) {
                            
                            dmg = CheckArmorDamage(person.BasicSkill() - mob.currentArmor);
                            DealMobDamage(dmg, mob);
                        }
                    }
                    break;

                case 2:

                    if (person is Berserker) {

                        if (person.ManaCountDown(100)) {

                            dmg = CheckArmorDamage(person.Skill1() - mob.currentArmor);
                            DealMobDamage(dmg, mob);
                 
                        }
                    }

                    if (person is Dicer) {

                        if (person.ManaCountDown(100)) {

                            dmg = CheckArmorDamage(person.Skill1() - mob.currentArmor);
                            person.CurrentMana -= 100;
                            DealMobDamage(dmg, mob);
                        }
                       
                    }
                    break;

                case 3:

                    //Isso aqui ta erradíssimo
                    if (person is Berserker) {

                        person.CurrentHP -= person.Skill2();
                        mob.HP = mob.HP / 2;
                    }

                    if (person is Dicer) {

                        if (person.ManaCountDown(150))
                        {
                            dmg = CheckArmorDamage(person.Skill2() - mob.currentArmor);
                            person.CurrentMana -= 100;
                            DealMobDamage(dmg, mob);

                        }

                        
                    }

                    break;


            }
            return dmg;
        }

        public static int MobTurn(Character person, Mob mob, int button)
        {
            dmgMob = CheckArmorDamage(mob.Skills() - person.CurrentArmor);
            person.CurrentHP -= dmgMob;

            //MOSTRAR VALOR (INT) DO PERSONAGEM E DO MOB
            Debug.WriteLine("Life person = " + person.CurrentHP + "\n" + "Life mob = " + mob.HP);

            return dmgMob;
        }
        public static int dmgTurnMob()
        {
            return dmgMob;
        }

        public static bool FinishBattle(Character person, Mob mob)
        {

            if (mob.IsDead())
            {
                int xpGain = 0;

                if (mob is Boss)
                {
                    xpGain = 70;
                }
                else
                {
                    xpGain = 30;
                }
                person.LevelUp(xpGain);
                WinBattle();
                Turn = 0;
                return false;

            }

            if (person.IsDead())
            {
                Debug.WriteLine("Mob ganhou!!!");
                Turn = 0;
                LoseBattle();
                return false;
            }

            return true;
        }

        public static void WinBattle()
        {
            ControllerGame.PlaySoundsVitorHugo("CaioYouWin.mp4");
          

        }

        public static void LoseBattle()
        {
            ControllerGame.PlaySoundsVitorHugo("CaioYouDied.mp4");
        }

        public static int CheckArmorDamage(int damage)
        {
            if (damage < 0)
            {
                return 0;
            }

            return damage;
        }

        public static int ReturnDmgTurn()
        {
            return dmg;
        }

        public static bool TurnMobAnimation()
        {
            if (Turn % 2 != 0) {
                return true;
            }
            else {
                return false;
            }

        }

        private static void DealMobDamage(int dmg, Mob mob)
        {
            mob.HP -= dmg;
        }
    }
}






