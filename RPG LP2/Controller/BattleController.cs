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



namespace RPG_LP2
{
    public static class BattleController
    {
        // public static Page TelaAtual { get; set; }

        static int Turn = 0;

        public static int InicializeBattle(Character person, Mob mob, int button)
        {
                
            return Turn;
            //CheckTurn(person, mob,turn,button);
        }
        // BattleScreen screen;
   
        public static void CheckTurn(Character person, Mob mob, int button, Button btn_actual) // checa se é o turno do mob ou do player
        {
           if (FinishBattle(person, mob)) 
            {
                Turn++;
                PlayerTurn(person, mob, button, btn_actual);
            
                Turn++;
                MobTurn(person, mob, button);    
            }

        }

        public static void PlayerTurn(Character person, Mob mob, int button, Button btn_actual)
        {
            Debug.WriteLine("Turno: " + Turn);
            switch (button) // OLHA ISSO AQUI
            {   
                case 1:

                    int damageTurn = person.BasicSkill() - mob.currentArmor;
                    Debug.WriteLine("Dano causado = " + damageTurn + "\n");
                    mob.HP -= damageTurn;
                    break;

                case 2:
                    //btn_actual.Content = person.BasicSkill().ToString;
                    break;

                
            }
        }

        public static void MobTurn(Character person, Mob mob, int button)
        {
            Debug.WriteLine("Life person = " + person.CurrentHP + "\n" + "Life mob = " + mob.HP);

            int damage = mob.Skills() - person.CurrentArmor;
            person.CurrentHP -= damage;
           
            //CheckTurn(person, mob,turn,button);
        }
            

        public static bool FinishBattle(Character person, Mob mob)
        {

            if (mob.IsDead())
            {
                int xpGain = 0;
                if (mob is Boss)
                {
                    xpGain = 50;
                }
                else
                {   
                    xpGain = 30; 
                }
                person.LevelUp(xpGain);
                WinBattle();
                return false;
                
                // chama musicazona, chama run 
            }

            if (person.IsDead())
            {
                Debug.WriteLine("Mob ganhou!!!");
                return false;
            }

            return true;
        }

        public static void WinBattle()
        {
            Debug.WriteLine("Você ganhou caio");

        }

        public static void LoseBattle(Character person, Mob mob)
        {

        }
    }
}


    


    
