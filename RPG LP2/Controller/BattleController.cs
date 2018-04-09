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
            CheckTurn(person,mob);
        }
       // BattleScreen screen;
        public static void CheckTurn(Character person, Mob mob) // checa se é o turno do mob ou do player
        {
            int cont = 0;

            if (cont % 2 == 0)
            {
                PlayerTurn(person,mob);
                cont++;
            }

            else
            {
                MobTurn(person,mob);
                cont++;
            }

        }

        public static void PlayerTurn(Character person, Mob mob)
        {

            if (true) // OLHA ISSO AQUI
            {
                Debug.WriteLine("Entrei aqui");
                mob.HP -= person.BasicSkill();

            }
        }

        public static void MobTurn(Character person, Mob mob)
        {
            person.CurrentHP -= mob.SkillBasic();

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
                person.LevelUp(xpGain);
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
            Debug.WriteLine("Voce venceu !!!");
            

        }

        public static void LoseBattle(Character person, Mob mob)
        {

        }
    }
}

    


    
