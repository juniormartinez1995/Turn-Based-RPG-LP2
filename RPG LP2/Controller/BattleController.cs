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

        public static int InicializeBattle(Character person, Mob mob, int button)
        {
                
            return Turn;
        }
        static DispatcherTimer timerPlayer = new DispatcherTimer();

        public static void CheckTurn(Character person, Mob mob, int button, Button btn_actual) // checa se é o turno do mob ou do player
        {
            if (FinishBattle(person, mob)) {
                Turn++;
                PlayerTurn(person, mob, button, btn_actual);
             
            }

            if (FinishBattle(person, mob)) {
                Turn++;
                MobTurn(person, mob, button);

            }          
        }

        public static void PlayerTurn(Character person, Mob mob, int button, Button btn_actual)
        {
            //MOSTRAR O NÚMERO DO TURNO
            Debug.WriteLine("Turno: " + Turn);

            switch (button) 
            {   
                case 1:
                    if (person is Berserker)
                    {
                        int damageTurn = CheckArmorDamage(person.BasicSkill() - mob.currentArmor);

                        //MOSTRAR O DANO CAUSADO NA TELA
                        Debug.WriteLine("Dano causado = " + damageTurn + "\n");
                        mob.HP -= damageTurn;
                    }
                    if (person is Dicer)
                    {
                        int damageTurn = CheckArmorDamage(person.BasicSkill() - mob.currentArmor);
                    }
                    break;

                case 2:
                    if(person.CurrentMana >= 100)
                    {
                        person.CurrentMana -= 100;
                        mob.HP -= person.Skill1();
                        Debug.WriteLine("Dano causado = " + person.Skill1() + "\n");
                    }
                    else
                    {
                        Debug.WriteLine("Você não tem mana o suficiente para castar essa habilidade");
                    }

                    break;

                case 3:
                    person.CurrentHP -= person.Skill2();
                    mob.HP = mob.HP/2;


                    break;

                
            }
        }

        public static void MobTurn(Character person, Mob mob, int button)
        {
            int damageTurn = CheckArmorDamage(mob.Skills() - person.CurrentArmor);
            person.CurrentHP -= damageTurn;

            //MOSTRAR VALOR (INT) DO PERSONAGEM E DO MOB
            Debug.WriteLine("Life person = " + person.CurrentHP + "\n" + "Life mob = " + mob.HP);

            //CheckTurn(person, mob,turn,button);
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
            Debug.WriteLine("Você ganhou caio");

        }

        public static void LoseBattle()
        {
            
        }

        public static int CheckArmorDamage(int damage)
        {
            if(damage < 0) 
            {
                return 0;
            }

            return damage;
        }

    }
}


    


    
