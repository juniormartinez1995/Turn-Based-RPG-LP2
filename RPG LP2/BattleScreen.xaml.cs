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

            WidthRatio = _Canvas.Width / 800;
            HeightRatio = _Canvas.Height / 600;

            InitialKnifePosition = 534 * WidthRatio;

        }

        DispatcherTimer MobAttackTimer = new DispatcherTimer();

        Character BattlePlayer;
        Mob Mob_;
        List<Object> CharList;
        DispatcherTimer timer = new DispatcherTimer();

        BitmapImage Ninja = new BitmapImage(new Uri(@"ms-appx:///Assets/BattleAnimations/NinjaServa.gif"));

        BitmapImage heart_stopped = new BitmapImage(new Uri(@"ms-appx:///Assets/heart_png.png"));
        BitmapImage heart_goON = new BitmapImage(new Uri(@"ms-appx:///Assets/heart_gif.gif"));


        int button = 0;
        int turn;
        double WidthRatio, HeightRatio, InitialKnifePosition;
        String ChosenSkill;
        bool AnimationEnabled, MobsDefeated;

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

        private void FirstButton(object sender, RoutedEventArgs e)
        {
            ChosenSkill = "FirstSkill";

            AnimationKnifeMob();
            btnSkillBasic.IsEnabled = false;
            btnSkillOne.IsEnabled = false;
            btnSkillTwo.IsEnabled = false;

        }
        private void SecondButton(object sender, RoutedEventArgs e)
        {
            ChosenSkill = "SecondSkill";

            AnimationKnifeMob();
            btnSkillBasic.IsEnabled = false;
            btnSkillOne.IsEnabled = false;
            btnSkillTwo.IsEnabled = false;

        }
        private void ThirdButton(object sender, RoutedEventArgs e)
        {
            ChosenSkill = "ThirdSkill";

            AnimationKnifeMob();
            btnSkillBasic.IsEnabled = false;
            btnSkillOne.IsEnabled = false;
            btnSkillTwo.IsEnabled = false;
        }

        public async void AnimationKnifeMob()
        {
            await Task.Delay(2700);

            if (MobsDefeated) return;

            Knife.Opacity = 100;

            if (!MobAttackTimer.IsEnabled) MobAttackTimer.Start();

            AnimationEnabled = true;
            Debug.WriteLine("QUANTAS VEZES ENTREI AQUI");

        }

        public void CastSkill(int Button)
        {
            if (!ControllerGame.IsSkillHittingEnemy(CharacterSkill, Mob1))
            {
                Canvas.SetLeft(CharacterSkill, Canvas.GetLeft(CharacterSkill) + 45);
                AttackingAnimation(true);

            }

            else if (ControllerGame.IsSkillHittingEnemy(CharacterSkill, Mob1))
            {
                BattleController.CheckTurn(BattlePlayer, Mob_, Button);
                PaintDamageGiven(1);
                AttackingAnimation(false);
                Canvas.SetLeft(CharacterSkill, Canvas.GetLeft(Person1) + 82);
                CharacterSkill.Source = null;
                ChosenSkill = null;
            }
        }



        //Evento para atualizar o progress bar
        private void AnimationHandler(object sender, object e)
        {

            if (hpBarCharacter.Value >= 0) hpBarCharacter.Value = BattlePlayer.CurrentHP;
            if (mpBarCharacter.Value >= 0) mpBarCharacter.Value = BattlePlayer.CurrentMana;
            if (hpBarMob.Value >= 0) hpBarMob.Value = Mob_.HP;
            if (BattlePlayer.CurrentHP < (BattlePlayer.MaxHealth / 2)) heart_icon.Source = heart_goON;

            switch (ChosenSkill)
            {
                case "FirstSkill":

                    if (BattlePlayer is Berserker)
                    {
                        CharacterSkill.Source = BattlePlayer.FirstSkill;
                        CastSkill(1);

                        // ControllerGame.PlaySoundSword("SoundSword.mp3");
                    }

                    if (BattlePlayer is Dicer)
                    {
                        CharacterSkill.Source = BattlePlayer.FirstSkill;
                        CastSkill(1);


                    }
                    break;

                case "SecondSkill":

                    if (BattlePlayer is Berserker)
                    {
                        CharacterSkill.Source = BattlePlayer.FirstSkill;
                        CastSkill(2);

                        // ControllerGame.PlaySoundSword("SoundSword.mp3");
                    }

                    if (BattlePlayer is Dicer)
                    {
                        CharacterSkill.Source = BattlePlayer.SecondSkill;
                        ControllerGame.PlaySoundsRPG("SnakeDicer.mp3");
                        CastSkill(2);

                    }
                    break;

                case "ThirdSkill":
                    if (BattlePlayer is Berserker)
                    {
                        CharacterSkill.Source = BattlePlayer.FirstSkill;
                        CastSkill(3);
                    }

                    if (BattlePlayer is Dicer)
                    {
                        CharacterSkill.Source = BattlePlayer.ThirdSkill;
                        //ControllerGame.PlaySoundsRPG("SnakeDicer.mp3");
                        CastSkill(3);
                    }
                    break;

            }

        }

        private void MobAttackHandler(object sender, object e)
        {
            if (MobsDefeated) return;
            if (!ControllerGame.IsSkillHittingPerson(Knife, Person1)) Canvas.SetLeft(Knife, Canvas.GetLeft(Knife) - 45);

            else if (ControllerGame.IsSkillHittingPerson(Knife, Person1))
            {
                Knife.Opacity = 0;
                Canvas.SetLeft(Knife, InitialKnifePosition);
                PaintDamageGiven(2);

                btnSkillBasic.IsEnabled = true;
                btnSkillOne.IsEnabled = true;
                btnSkillTwo.IsEnabled = true;
                AnimationEnabled = false;
                MobAttackTimer.Stop();

            }

        }

        public async void PaintDamageGiven(int Attacker)
        {
            // 1 = Player ataca
            // 2 = Mob Ataca
            switch (Attacker)
            {
                case 1:
                    Hitbox.Opacity = 100;
                    Hitbox.Text = BattleController.ReturnDmgTurn().ToString();
                    await Task.Delay(600);
                    Hitbox.Opacity = 0;
                    break;

                case 2:
                    HitboxPerson.Opacity = 100;
                    HitboxPerson.Text = BattleController.dmgTurnMob().ToString();
                    await Task.Delay(600);
                    HitboxPerson.Opacity = 0;
                    break;
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
            MobsDefeated = true;
            DisplayEndedBattleDialog();
        }

        //Evento para tratar quando o personagem morre
        private void BattlePlayer_CharacterDead(object sender, EventArgs args)
        {
            UnsignPageEvents();
            this.Frame.Navigate(typeof(LosePage), BattlePlayer);
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

            timer.Tick += AnimationHandler;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 40);
            timer.Start();

            MobAttackTimer.Tick += MobAttackHandler;
            MobAttackTimer.Interval = new TimeSpan(0, 0, 0, 0, 30);

        }

        //Cancela todos os eventos da página
        public void UnsignPageEvents()
        {
            Mob_.MobDead -= Mob__MobDead;
            BattlePlayer.CharacterDead -= BattlePlayer_CharacterDead;
            BattlePlayer.NoMana -= BattlePlayer_NoMana;

            timer.Tick -= AnimationHandler;
            timer.Stop();

            MobAttackTimer.Tick -= MobAttackHandler;

        }
    }
}
