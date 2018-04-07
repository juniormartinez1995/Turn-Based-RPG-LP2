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
        public static Page TelaAtual { get; set; }


        public static void CheckTurn(Character person, Mob mob, BattleScreen battlescreen) // checa se é o turno do mob ou do player
        {
            int cont = 0;

            if (cont % 2 == 0)
            {
                TurnMob(person, mob, battlescreen);
                cont++;
            }

            else
            {
                TurnPlayer(person, mob, battlescreen);
                cont++;
            }

        }

        public static void TurnPlayer(Character person, Mob mob, BattleScreen battlescreen)
        {
            if (battlescreen.btnSkillBasicClicked() == 1) // OLHA ISSO AQUI
            {
                mob.HP -= person.SkillBasic();
                Debug.WriteLine(mob.HP);
            }
        }

        public static void TurnMob(Character person, Mob mob, BattleScreen battlescreen)
        {
            person.currentHP -= mob.SkillBasic();

        }

        public static bool FinishBattle(Character person, Mob mob)
        {
           
            if (mob.IsDead())
            {
                int xpGain = 0;
                if (mob is Boss)
                {
                    xpGain = 50;
                    //person.currentXP += 50;

                }
                else
                {
                    xpGain = 30;
                    //person.currentXP += 30;
                }
                person.upLevel(xpGain);
                return true;
                //person.upLevel(mob, new EventArgs()) += person.Level;
                // chama musicazona, chama run 
            }

            if (person.IsDead())
            {
                Debug.WriteLine("hey b0ss");
                return true;
            }

            return false;
        }

        public static void WinBattle(Character person, Mob mob)
        {
           

        }

        public static void LoseBattle(Character person, Mob mob)
        {
            
        }
    }
}

    
