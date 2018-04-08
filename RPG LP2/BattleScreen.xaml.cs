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

using RPGlib.Mobs;
using Windows.UI.Xaml.Media.Imaging;

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
            inicializateBattle();
            // BattleController.TelaAtual = this;           
            BattleAnimation();
            Mob1.Source = Ninja;
 
          
        }

        Character BattlePlayer;
        Mob mob;
        DispatcherTimer Timer = new DispatcherTimer();
        BitmapImage Ninja = new BitmapImage(new Uri(@"ms-appx:///Assets/BattleAnimations/NinjaServa.gif"));
    



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            BattlePlayer = e.Parameter as Character;
            mob = e.Parameter as Mob;

            hpBarCharacter.Maximum = BattlePlayer.maxHealth;
            hpBarCharacter.Value = 100;
            
          
        }

        //Sem uso por enquanto
        public void BattleAnimation()
        {
            Timer.Tick += Timer_Tick;
            Timer.Interval = new TimeSpan(0, 0, 0, 50);
            Timer.Start();
        }

        //Sem uso por enquanto
        private void Timer_Tick(object sender, object e)
        {
            Mob1.Source = Ninja;

        }

        

        private void LeaveBtn_Tapped(object sender, TappedRoutedEventArgs e)
        { 
            this.Frame.Navigate(typeof(Map), BattlePlayer);
        }

        public void btnSkillBasic_Click(object sender, RoutedEventArgs e)
        {
            btnSkillBasicClicked();
        }
        public int btnSkillBasicClicked()
        {
            return 1;
        }
        public void inicializateBattle()
        {
           
            BattleController.InicializeBattle2(BattlePlayer,mob);
        }
    }
   
}
