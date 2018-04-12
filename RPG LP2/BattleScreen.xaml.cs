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
using System.Diagnostics;

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
            Mob1.Source = Ninja;
            StartTimer();
        }

        Character BattlePlayer;
        Mob mob;
        DispatcherTimer timer = new DispatcherTimer();
        BitmapImage Ninja = new BitmapImage(new Uri(@"ms-appx:///Assets/BattleAnimations/NinjaServa.gif"));

        int button = 0;
        int turn;

        private void Mob_MobDead(object sender, EventArgs args)
        {
            DisplayEndedBattleDialog();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            List<Object> CharList = e.Parameter as List<Object>;

            BattlePlayer = CharList.ElementAt(0) as Character;
            mob = CharList.ElementAt(1) as Mob;

            Debug.WriteLine("DANO MOB: " +mob.Damage);
            Debug.WriteLine("Dano Player " +BattlePlayer.Damage);


            hpBarCharacter.Maximum = BattlePlayer.MaxHealth;
            hpBarCharacter.Value = BattlePlayer.CurrentHP;
            mpBarCharacter.Maximum = BattlePlayer.MaxMana;
            mpBarCharacter.Value = BattlePlayer.CurrentMana;
            hpBarMob.Maximum = mob.HP;
            hpBarMob.Value = mob.HP;

            turn = BattleController.InicializeBattle(BattlePlayer, mob, button);


            mob.MobDead += Mob_MobDead;
        }
        private async void DisplayEndedBattleDialog()
        {
            ContentDialog BattleEnded = new ContentDialog
            {
                Title = "FIM DA BATALHA",
                Content = "Você venceu!!!",
                CloseButtonText = "Voltar ao mapa"

            };

            ContentDialogResult result = await BattleEnded.ShowAsync();
            this.Frame.Navigate(typeof(Map), BattlePlayer);
        }

        public void StartTimer()
        {
            if (!timer.IsEnabled)
            {
                timer.Tick += Timer_Tick;
                timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
                timer.Start();
            }
        }

        private void Timer_Tick(object sender, object e)
        {
            if (hpBarCharacter.Value >= 0) hpBarCharacter.Value = BattlePlayer.CurrentHP;
            if (mpBarCharacter.Value >= 0) mpBarCharacter.Value = BattlePlayer.CurrentMana;
            if (hpBarMob.Value >= 0) hpBarMob.Value = mob.HP;
        }

        private void LeaveBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Map), BattlePlayer);
        }

        public void BtnBasicSkill_Click(object sender, RoutedEventArgs e)
        {
            BattleController.CheckTurn(BattlePlayer, mob, 1, btnSkillBasic);
        }

        private void BtnSkillOne_Click(object sender, RoutedEventArgs e)
        {
            BattleController.CheckTurn(BattlePlayer, mob, 2, btnSkillOne);
        }
    }
}
