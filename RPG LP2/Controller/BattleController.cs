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

        public static void InicializeBattle(Character person, Mob mob)
        {
            int turn = 0;
            CheckTurn(person, mob,turn);
        }
        // BattleScreen screen;
   
        public static void CheckTurn(Character person, Mob mob,int turn) // checa se é o turno do mob ou do player
        {
            while (FinishBattle(person, mob)) 
            {

                if (turn % 2 == 0) 
                {
                    turn++;
                    PlayerTurn(person, mob,turn);
                }

                else 
                {
                    turn++;
                    MobTurn(person, mob, turn);    
                }

            }

        }

        public static void PlayerTurn(Character person, Mob mob,int turn)
        {
           // if (true) // OLHA ISSO AQUI
          //  {
            Debug.WriteLine("Life Mob");
            mob.HP -= person.BasicSkill();
            Debug.WriteLine(mob.HP);
            CheckTurn(person,mob,turn);
          //  }
        }

        public static void MobTurn(Character person, Mob mob, int turn)
        {
            Debug.WriteLine("Life person");
            person.CurrentHP -= mob.Skills();
            Debug.WriteLine(person.CurrentHP);
            CheckTurn(person, mob,turn);
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

        public static void WinBattle(Character person, Mob mob)
        {
               


        }

        public static void LoseBattle(Character person, Mob mob)
        {

        }
    }
}


    


    
