﻿using RPGlib.Characters;
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
using RPG_LP2.Controller;

namespace RPG_LP2
{
    public static class BattleController
    {
        // public static Page TelaAtual { get; set; }

        static int Turn = 0;
        static int dmg = 0;
        static int dmgturn = 0;
        static int dmgMob = 0;
        static int multicast = 1;
        static int local = 0;

        public static int InicializeBattle(Character person, Mob mob, int button)
        {

            return Turn;
        }
        static DispatcherTimer timerPlayer = new DispatcherTimer();

        public async static void CheckTurn(Character person, Mob mob, int button) // checa se é o turno do mob ou do player
        {

            //Turno do player
            Turn++;
            PlayerTurn(person, mob, button);


            //Turno do mob
            if (mob.HP > 0)
            {
                Turn++;

                await Task.Delay(2700);
                TurnMobAnimation();
                MobTurn(person, mob, button);
                if (person is Berserker)
                {
                    SoundController.PlayDynamicSound("Battle_Trance.mp3");
                }

            }


        }

        public static int PlayerTurn(Character person, Mob mob, int button)
        {
            //MOSTRAR O NÚMERO DO TURNO
            Debug.WriteLine("Turno: " + Turn);


            switch (button)
            {
                case 1:
                    if (person is Berserker)
                    {

                        dmg = CheckArmorDamage(person.BasicSkill() - mob.currentArmor);
                        DealMobDamage(dmg, mob);
                    }
                    if (person is Dicer)
                    {

                        dmgturn = person.BasicSkill();
                        multicast = dmgturn / person.Damage;
                        dmg = CheckArmorDamage(dmgturn - mob.currentArmor);
                        DealMobDamage(dmg, mob);
                        //Checagem se houve multicast
                        if (multicast > 1)
                        {
                            if (multicast == 2)
                            {
                                SoundController.PlayDynamicSound("Multicast_x2.mp3");
                            }
                            if (multicast == 3)
                            {
                                SoundController.PlayDynamicSound("Multicast_x3.mp3");
                            }
                            if (multicast == 4)
                            {
                                SoundController.PlayDynamicSound("Multicast_x4.mp3");
                            }
                            if (multicast == 5)
                            {
                                SoundController.PlayDynamicSound("Multicast_x3.mp3");
                                SoundController.PlayDynamicSound("Multicast_x4.mp3");
                            }
                        }
                    }
                    break;

                case 2:

                    if (person is Berserker)
                    {

                        if (person.ManaCountDown(100))
                        {

                            mob.currentArmor -= 5;
                            dmg = CheckArmorDamage(person.Skill1() - mob.currentArmor);
                            DealMobDamage(dmg, mob);

                        }
                        else
                        {
                            SoundController.PlayDynamicSound("BerserkerNoMana.mp3");
                        }
                    }

                    if (person is Dicer)
                    {

                        if (person.ManaCountDown(0))
                        {
                            person.Skill1();
                        }

                    }
                    break;

                case 3:

                    if (person is Berserker)
                    {

                        person.CurrentHP -= person.Skill2();
                        local = mob.HP / 2;
                        mob.HP = mob.HP / 2;
                        dmg = local;
                    }

                    if (person is Dicer)
                    {

                        if (person.ManaCountDown(150))
                        {
                            dmgturn = person.Skill2();
                            multicast = dmgturn / 80;
                            dmg = CheckArmorDamage(dmgturn - mob.currentArmor);
                            person.CurrentMana -= 100;
                            DealMobDamage(dmg, mob);
                            //Checagem se houve multicast
                            if (multicast > 1)
                            {
                                if (multicast == 2)
                                {
                                    SoundController.PlayDynamicSound("Multicast_x2.mp3");
                                }
                                if (multicast == 3)
                                {
                                    SoundController.PlayDynamicSound("Multicast_x3.mp3");
                                }
                                if (multicast == 4)
                                {
                                    SoundController.PlayDynamicSound("Multicast_x4.mp3");
                                }
                                if (multicast == 5)
                                {
                                    SoundController.PlayDynamicSound("Multicast_x3.mp3");
                                    SoundController.PlayDynamicSound("Multicast_x4.mp3");
                                }
                            }

                        }



                    }

                    break;


            }
            return dmg;
        }

        public static int MobTurn(Character person, Mob mob, int button)
        {
            if (mob.HP < 50) mob.CurrentStrategy = new HardStrategy();
            else if (mob.HP > 100) mob.CurrentStrategy = new MediumStrategy();
            else mob.CurrentStrategy = new EasyStrategy();


            dmgMob = CheckArmorDamage(mob.Fight() - person.CurrentArmor);
            person.CurrentHP -= dmgMob;
            //MOSTRAR VALOR (INT) DO PERSONAGEM E DO MOB
            Debug.WriteLine("Life person = " + person.CurrentHP + "\n" + "Life mob = " + mob.HP);

            return dmgMob;
        }
        public static int dmgTurnMob()
        {
            return dmgMob;
        }
        //Caio é trouxa xd xd xd
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
                    xpGain = 110;
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
            SoundController.PlayDynamicSound("CaioYouWin.mp4");


        }

        public static void LoseBattle()
        {
            SoundController.PlayDynamicSound("CaioYouDied.mp4");
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

        public static int ReturnMulticastTurn()
        {
            return multicast;
        }

        public static bool TurnMobAnimation()
        {
            if (Turn % 2 != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private static void DealMobDamage(int dmg, Mob mob)
        {
            mob.HP -= dmg;
        }
    }
}






