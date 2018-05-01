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
            ControllerGame.AdjustFullScreenMode(_Canvas, this);
            Mob1.Source = Ninja;
            heart_icon.Source = heart_stopped;
        }

        //Timer HitBox
        DispatcherTimer TimerHitbox = new DispatcherTimer();

        //Timer Berserker
        DispatcherTimer TimerSword = new DispatcherTimer();

        //Timer Dicer
        DispatcherTimer TimerSnakeDicer = new DispatcherTimer();
        DispatcherTimer TimerGhostDicer = new DispatcherTimer();
        DispatcherTimer TimerWaterDicer = new DispatcherTimer();

        //Timer Mob
        DispatcherTimer TimerKnife = new DispatcherTimer();
        DispatcherTimer TimerHitBoxPerson = new DispatcherTimer();


        Character BattlePlayer;
        Mob Mob_;
        List<Object> CharList;
        DispatcherTimer timer = new DispatcherTimer();
        BitmapImage Ninja = new BitmapImage(new Uri(@"ms-appx:///Assets/BattleAnimations/NinjaServa.gif"));

        BitmapImage heart_stopped = new BitmapImage(new Uri(@"ms-appx:///Assets/heart_png.png"));
        BitmapImage heart_goON = new BitmapImage(new Uri(@"ms-appx:///Assets/heart_gif.gif"));

        int button = 0;
        int turn;
        int cont = 0;
        int cont1 = 0;

        private async void DisplayEndedBattleDialog()
        {
            Debug.WriteLine("DEBUG AQUIIIII");
            ContentDialog BattleEnded = new ContentDialog
            {
                Title = "FIM DA BATALHA",
                Content = "Você venceu!!!",
                CloseButtonText = "Voltar ao mapa"

            };

            ContentDialogResult result = await BattleEnded.ShowAsync();
            UnsignPageEvents();
            this.Frame.Navigate(typeof(Map), CharList);
        }

        //Setar animação de ataque ou idle
        private void AttackingAnimation(bool isAttacking)
        {
            if (isAttacking)
            {
                Person1.Source = BattlePlayer.Attacking;
            }
            else
            {
                Person1.Source = BattlePlayer.IdleRight;
            }
        }

        //Define valores máximos e mínimos para as progress bars
        public void AdjustProgessBar()
        {
            hpBarCharacter.Maximum = BattlePlayer.MaxHealth;
            hpBarCharacter.Value = BattlePlayer.CurrentHP;
            mpBarCharacter.Maximum = BattlePlayer.MaxMana;
            mpBarCharacter.Value = BattlePlayer.CurrentMana;
            hpBarMob.Maximum = Mob_.HP;
            hpBarMob.Value = Mob_.HP;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (ControllerGame.CheckLastPage(typeof(Map), this))
            {
                CharList = e.Parameter as List<Object>;

                BattlePlayer = CharList.ElementAt(0) as Character;
                Mob_ = CharList.ElementAt(1) as Mob;
                CharList.Clear();

                Debug.WriteLine("DANO MOB: " + Mob_.Damage);
                Debug.WriteLine("Dano Player " + BattlePlayer.Damage);
                Debug.WriteLine("EU SOU " + Mob_.name);

                AdjustProgessBar();

                Person1.Source = BattlePlayer.IdleUp;

            }

            SignPageEvents();
            turn = BattleController.InicializeBattle(BattlePlayer, Mob_, button);

        }

        //Evento para sair da página e voltar ao mapa
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

            UnsignPageEvents();
            this.Frame.Navigate(typeof(Map), CharList);
        }


        public void BtnBasicSkill_Click(object sender, RoutedEventArgs e)
        {

            if (BattlePlayer is Berserker)
            {
                Sword.Opacity = 100;
                TimerSword.Start();
                TimerHitbox.Start();
                AnimationKnifeMob();

                // ControllerGame.PlaySoundSword("SoundSword.mp3");
            }

            if (BattlePlayer is Dicer)
            {

                WaterDicer.Opacity = 100;
                TimerWaterDicer.Start();

                AnimationKnifeMob();

                TimerHitbox.Start();

            }

        }

        private void BtnSkillOne_Click(object sender, RoutedEventArgs e)
        {
            //BattleController.CheckTurn(BattlePlayer, Mob_, 2, btnSkillOne);

            if (BattlePlayer is Berserker)
            {
                Sword.Opacity = 100;
                TimerSword.Start();
                TimerHitbox.Start();
                AnimationKnifeMob();

                // ControllerGame.PlaySoundSword("SoundSword.mp3");
            }

            if (BattlePlayer is Dicer)
            {

                /*
                Fireball.Opacity = 100;
                ControllerGame.PlaySoundsRPG("Fireball.mp3");
                TimerFireball.Start();
                */

                SnakeDicer.Opacity = 100;
                ControllerGame.PlaySoundsRPG("SnakeDicer.mp3");
                TimerSnakeDicer.Start();
                TimerHitbox.Start();
                AnimationKnifeMob();

            }

        }

        private void BtnSkillTwo_Click(object sender, RoutedEventArgs e)
        {
            if (BattlePlayer is Berserker)
            {
                BattleController.CheckTurn(BattlePlayer, Mob_, 3, btnSkillTwo);
                TimerHitbox.Start();
                AnimationKnifeMob();
            }

            if (BattlePlayer is Dicer)
            {
                GhostDicer.Opacity = 100;
                //ControllerGame.PlaySoundsRPG("SnakeDicer.mp3");
                TimerGhostDicer.Start();
                TimerHitbox.Start();
                AnimationKnifeMob();

            }
        }

        public async void AnimationKnifeMob()
        {
            //if (BattleController.TurnMobAnimation()) 

            if (Mob_ is Ninja) { await Task.Delay(1200); Knife.Opacity = 100; TimerKnife.Start(); }
            if (Mob_ is PablloVittar) { await Task.Delay(1200); Knife.Opacity = 100; TimerKnife.Start(); }
            TimerHitBoxPerson.Start();

        }

        //Evento para atualizar o progress bar
        private void Timer_Tick(object sender, object e)
        {

            if (hpBarCharacter.Value >= 0) hpBarCharacter.Value = BattlePlayer.CurrentHP;
            if (mpBarCharacter.Value >= 0) mpBarCharacter.Value = BattlePlayer.CurrentMana;
            if (hpBarMob.Value >= 0) hpBarMob.Value = Mob_.HP;
            if (BattlePlayer.CurrentHP < (BattlePlayer.MaxHealth / 2)) heart_icon.Source = heart_goON;
        }

        //ANIMAÇÕES DAS SPELLS DO DICER--------------------------------------------------------------------------------------------------

        public void TimerWater_Tick(object sender, object e)
        {
            if (!ControllerGame.IsSkillHittingEnemy(WaterDicer, Mob1))
            {
                Canvas.SetLeft(WaterDicer, Canvas.GetLeft(WaterDicer) + 45);
                AttackingAnimation(true);
            }
            else if (ControllerGame.IsSkillHittingEnemy(WaterDicer, Mob1))
            {
                BattleController.CheckTurn(BattlePlayer, Mob_, 1, btnSkillBasic);
                AttackingAnimation(false);
                Canvas.SetLeft(WaterDicer, Canvas.GetLeft(Person1) + 82);
                WaterDicer.Opacity = 0;
                TimerWaterDicer.Stop();

            }
        }

        public void TimerSnake_Tick(object sender, object e)
        {
            if (!ControllerGame.IsSkillHittingEnemy(SnakeDicer, Mob1))
            {
                Canvas.SetLeft(SnakeDicer, Canvas.GetLeft(SnakeDicer) + 45);
                AttackingAnimation(true);
            }

            else if (ControllerGame.IsSkillHittingEnemy(SnakeDicer, Mob1))
            {
                BattleController.CheckTurn(BattlePlayer, Mob_, 2, btnSkillOne);
                AttackingAnimation(false);
                Canvas.SetLeft(SnakeDicer, Canvas.GetLeft(Person1) + 82);
                SnakeDicer.Opacity = 0;
                TimerSnakeDicer.Stop();

            }
        }

        public void TimerGhost_Tick(object sender, object e)
        {
            if (!ControllerGame.IsSkillHittingEnemy(GhostDicer, Mob1))
            {
                Canvas.SetLeft(GhostDicer, Canvas.GetLeft(GhostDicer) + 45);
                AttackingAnimation(true);
            }
            else if (ControllerGame.IsSkillHittingEnemy(GhostDicer, Mob1))
            {
                BattleController.CheckTurn(BattlePlayer, Mob_, 3, btnSkillTwo);
                AttackingAnimation(false);
                Canvas.SetLeft(GhostDicer, Canvas.GetLeft(Person1) + 82);
                GhostDicer.Opacity = 0;
                TimerGhostDicer.Stop();

            }
        }

        //ANIMAÇÕES DAS SPELLS DO BERSERKER--------------------------------------------------------------------------------------------------

        public void TimerSword_Tick(object sender, object e)
        {

            if (!ControllerGame.IsSkillHittingEnemy(Sword, Mob1))
            {
                Canvas.SetLeft(Sword, Canvas.GetLeft(Sword) + 45);
                AttackingAnimation(true);
            }
            else if (ControllerGame.IsSkillHittingEnemy(Sword, Mob1))
            {
                BattleController.CheckTurn(BattlePlayer, Mob_, 1, btnSkillBasic);
                AttackingAnimation(false);
                Canvas.SetLeft(Sword, Canvas.GetLeft(Person1) + 82);
                Sword.Opacity = 0;
                TimerSword.Stop();

            }

        }

        //ANIMAÇÃO DAS SKILLS DO MOB ------------------------------------------------------
        public void TimerKnife_Tick(object sender, object e)
        {
            if (!ControllerGame.IsSkillHittingPerson(Knife, Person1)) Canvas.SetLeft(Knife, Canvas.GetLeft(Knife) - 45);

            else if (ControllerGame.IsSkillHittingPerson(Knife, Person1))
            {
                Canvas.SetLeft(Knife, Canvas.GetLeft(Person1) + 740);
                Knife.Opacity = 0;
                TimerKnife.Stop();

            }
        }

        //Evento para tratar o dano sofrido do mob e mostrar na tela
        public void TimerHitboxMob_Tick(object sender, object e)
        {
            if (!ControllerGame.IsSkillHittingEnemy(Sword, Mob1))
            {
                Hitbox.Opacity = 100;

                Hitbox.Text = BattleController.ReturnDmgTurn().ToString();

                cont++;
                if (cont == 2)
                {
                    Hitbox.Opacity = 0;
                    cont = 0;
                    TimerHitbox.Stop();

                }
            }
        }

        //Evento para tratar o dano sofrido do jogador e mostrar na tela
        public void TimerHitboxPerson_Tick(object sender, object e)
        {
            if (!ControllerGame.IsSkillHittingEnemy(Knife, Person1))
            {
                HitboxPerson.Opacity = 100;

                HitboxPerson.Text = BattleController.dmgTurnMob().ToString();

                cont1++;
                if (cont1 == 2)
                {
                    HitboxPerson.Opacity = 0;
                    cont1 = 0;
                    TimerHitBoxPerson.Stop();
                }
            }

        }

        //Evento para tratar quando o mob morre
        private void Mob__MobDead(object sender, EventArgs args)
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

        //Evento para tratar quando o personagem morre
        private void BattlePlayer_CharacterDead(object sender, EventArgs args)
        {
            UnsignPageEvents();
            this.Frame.Navigate(typeof(LosePage));
        }

        //Evento para tratar quando o jogador está sem mana
        private void BattlePlayer_NoMana(object sender, EventArgs args)
        {

            btnSkillBasic.Visibility = 0;
            btnSkillBasic.Opacity = 0;

            btnSkillOne.Opacity = 0;
            btnSkillOne.Visibility = 0;

            btnSkillTwo.Opacity = 0;
            btnSkillTwo.Visibility = 0;
        }

        //Assina todos os eventos da página
        public void SignPageEvents()
        {
            Mob_.MobDead += Mob__MobDead;
            BattlePlayer.CharacterDead += BattlePlayer_CharacterDead;
            BattlePlayer.NoMana += BattlePlayer_NoMana;

            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Start();

            TimerSword.Tick += TimerSword_Tick;
            TimerSword.Interval = new TimeSpan(0, 0, 0, 0, 40);

            TimerWaterDicer.Tick += TimerWater_Tick;
            TimerWaterDicer.Interval = new TimeSpan(0, 0, 0, 0, 40);

            TimerSnakeDicer.Tick += TimerSnake_Tick;
            TimerSnakeDicer.Interval = new TimeSpan(0, 0, 0, 0, 40);

            TimerGhostDicer.Tick += TimerGhost_Tick;
            TimerGhostDicer.Interval = new TimeSpan(0, 0, 0, 0, 40);

            TimerHitbox.Tick += TimerHitboxMob_Tick;
            TimerHitbox.Interval = new TimeSpan(0, 0, 0, 1, 0);

            TimerKnife.Tick += TimerKnife_Tick;
            TimerKnife.Interval = new TimeSpan(0, 0, 0, 0, 40);

            TimerHitBoxPerson.Tick += TimerHitboxPerson_Tick;
            TimerHitBoxPerson.Interval = new TimeSpan(0, 0, 0, 1, 0);

        }

        //Cancela todos os eventos da página
        public void UnsignPageEvents()
        {
            Mob_.MobDead -= Mob__MobDead;
            BattlePlayer.CharacterDead -= BattlePlayer_CharacterDead;
            BattlePlayer.NoMana -= BattlePlayer_NoMana;

            timer.Tick -= Timer_Tick;
            timer.Stop();

            TimerSword.Tick -= TimerSword_Tick;

            TimerWaterDicer.Tick -= TimerWater_Tick;
            TimerSnakeDicer.Tick -= TimerSnake_Tick;
            TimerGhostDicer.Tick -= TimerGhost_Tick;

            TimerHitbox.Tick -= TimerHitboxMob_Tick;
            TimerKnife.Tick -= TimerKnife_Tick;
            TimerHitBoxPerson.Tick -= TimerHitboxPerson_Tick;
        }
    }
}
