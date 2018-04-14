﻿using System;
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
using System.Threading.Tasks;

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
        List<Object> CharList;
        Mob Mob_;
        DispatcherTimer timer = new DispatcherTimer();
        BitmapImage Ninja = new BitmapImage(new Uri(@"ms-appx:///Assets/BattleAnimations/NinjaServa.gif"));

        BitmapImage heart_stopped = new BitmapImage(new Uri(@"ms-appx:///Assets/heart_png.png"));
        BitmapImage heart_goON = new BitmapImage(new Uri(@"ms-appx:///Assets/heart_gif.gif"));

        int button = 0;
        int turn;

        private void Mob_MobDead(object sender, EventArgs args)
        {
            if (CharList.Count == 0)
            {
                CharList.Add(BattlePlayer);
                CharList.Add(Mob_);
            }
            else
            {
                CharList.Clear();
                CharList.Add(BattlePlayer);
                CharList.Add(Mob_);
            }
            DisplayEndedBattleDialog();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CharList = e.Parameter as List<Object>;

            BattlePlayer = CharList.ElementAt(0) as Character;
            Mob_ = CharList.ElementAt(1) as Mob;
            CharList.Clear();

            Debug.WriteLine("DANO MOB: " + Mob_.Damage);
            Debug.WriteLine("Dano Player " + BattlePlayer.Damage);
            Debug.WriteLine("EU SOU " + Mob_.name);

            hpBarCharacter.Maximum = BattlePlayer.MaxHealth;
            hpBarCharacter.Value = BattlePlayer.CurrentHP;
            mpBarCharacter.Maximum = BattlePlayer.MaxMana;
            mpBarCharacter.Value = BattlePlayer.CurrentMana;
            hpBarMob.Maximum = Mob_.HP;
            hpBarMob.Value = Mob_.HP;

            heart_icon.Source = heart_stopped;

            mob.MobDead += Mob_MobDead;

            turn = BattleController.InicializeBattle(BattlePlayer, Mob_, button);
            StartTimer();

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
            this.Frame.Navigate(typeof(Map), CharList);
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
            if (BattlePlayer.CurrentHP < (BattlePlayer.MaxHealth / 2)) heart_icon.Source = heart_goON;
        }

        private void LeaveBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (CharList.Count == 0)
            {
                CharList.Add(BattlePlayer);
                CharList.Add(Mob_);
            }
            else
            {
                CharList.Clear();
                CharList.Add(BattlePlayer);
                CharList.Add(Mob_);
            }

            this.Frame.Navigate(typeof(Map), CharList);
        }

        public void BtnBasicSkill_Click(object sender, RoutedEventArgs e)
        {
            BattleController.CheckTurn(BattlePlayer, Mob_, 1, btnSkillBasic);
        }

        private void BtnSkillOne_Click(object sender, RoutedEventArgs e)
        {
            BattleController.CheckTurn(BattlePlayer, Mob_, 2, btnSkillOne);
        }
    }
}
