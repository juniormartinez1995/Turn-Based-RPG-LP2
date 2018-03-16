using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using RPGlib.Characters;
using RPGlib;
using RPG_LP2;
using RPGlib.Battle;
using RPGlib.Mobs;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace RPG_LP2
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class BattleScreen : Page
    {
       
        public BattleScreen()
        {
            this.InitializeComponent();
          
        }
        int cont = 0;
        Character p1;
        Mob mob;
      /*
        private bool ButtonSkillBasic(object sender, TappedRoutedEventArgs e)
        {
            cont++;

            //CheckTurn();
            return  true;
        }
        /*
        public void CheckTurn() // checa se é o turno do mob ou do player
        {
            if (cont % 2 == 0)
            {
                TurnMob(p1,mob);
            }
            
            else
            {
                TurnPlayer(p1,mob);
            }
    
        }
        */
        /*
        public void TurnPlayer(Character p1, Mob mob)
        {
            if (ButtonSkillBasic == true) // se o botão clicado foi o Ataque básico
            {
              if (p1 is Berserker b1)
                {
                   mob.HP -= b1.SkillB();

                }
            }
        }
        */
        public void TurnMob(Character p1,Mob mob)
        {

        }

     
    }
}
