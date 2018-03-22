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

namespace RPG_LP2
{
    public static class BattleController
    {
        public static Page TelaAtual { get; set; }

        public static void ButtonSkillBasic_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("alo");
            //(TelaAtual as BattleScreen).MeuButão.;
        }


        public static void CheckTurn(int cont, Character person, Mob mob) // checa se é o turno do mob ou do player
        {
            if (cont % 2 == 0)
            {
                TurnMob(person, mob);
            }

            else
            {
                TurnPlayer(person, mob);
            }

        }

        public static void TurnPlayer(Character person, Mob mob)
        {
            if (true) // OLHA ISSO AQUI
            {
                mob.HP -= person.SkillBasic();
            }
        }

        public static void TurnMob(Character p1, Mob mob)
        {

        }

    }
}
