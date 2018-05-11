using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using RPGlib.Characters;
using Windows.UI.Core;
using RPGlib.Itens;
using System.Diagnostics;
using RPGlib;
using RPGlib.Mobs;
using RPG_LP2.Controller;


// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace RPG_LP2
{

    public sealed partial class Map : Page
    {
        public Map()
        {

            this.InitializeComponent();
            ControllerGame.AdjustFullScreenMode(_Canvas, this);

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;

            this.NavigationCacheMode = NavigationCacheMode.Enabled;

            StartAnimation(); //Startar a animação
            SetCollision(); //Inicialização das colisões pré-definidas
            SetChetsInMap(); //Inicialização dos baús pré-definidos
            AddImageOnList(); //Inicializa as imagens do Inventário do mapa em um List
            SetEnemiesPosition(); //Inicializa os inimigos
            SetKeyOnMap();

            Generator.ChestPopulate(ChestControl); //Método para gerar os itens randomicamente dentro do baú

            WidthRatio = _Canvas.Width / 800;
            HeightRatio = _Canvas.Height / 600;

            SoundController.PlaySound(VoiceSound, "CaioAbraOBaivis.mp4");


        }

        DispatcherTimer timer = new DispatcherTimer(); //Timer da animação
        List<Image> Collision = new List<Image>(); //Lista de colisões no mapa
        List<Image> LockedChests = new List<Image>(); //Lista de baús no mapa
        List<Image> Enemies = new List<Image>(); //Lista de enimigos no mapa
        List<Image> DroppedKeys = new List<Image>();//Lista de chaves no mapa
        List<object> MobAndChar = new List<object>();
        List<InventoryBitImage> ListInvetoryImage = new List<InventoryBitImage>(); //Lista das Imagens de Inventário


        Character Player; //Personagem que estará no mapa
        PablloVittar PablloVittar;
        Ninja Ninja;
        Chest ChestControl = new Chest(); //Gerenciamento do baú

        //Ninja Ninja = new Ninja();

        double WidthRatio, HeightRatio;
        double XSpeed, YSpeed;
        double PosY, PosX; //Posição X e Y do personagem no mapa
        bool Up, Down, Right, Left, IsAnotherPage; //Checagem da direção que o personagem está indo


        public void StoreChars(Mob EnemyMob)
        {
            MobAndChar.Add(EnemyMob);
        }

        //Método para setar os baús no mapa
        public void SetChetsInMap()
        {
            LockedChests.Add(Chest0);
        }

        //Método para setar os inimigos no mapa
        public void SetEnemiesPosition()
        {
            Enemies.Add(NinjaIMG);
            Enemies.Add(PabbloVitarIMG);

        }

        //Método para setar as colisões no mapa
        public void SetCollision()
        {
            Collision.Add(Collision0);
            Collision.Add(Collision1);
            Collision.Add(Collision2);
            Collision.Add(Collision3);
            Collision.Add(MapExit);
        }

        public void SetKeyOnMap()
        {
            DroppedKeys.Add(FirstMapKey);
        }

        //Método para adicionar as imagens a uma list
        private void AddImageOnList()
        {
            ListInvetoryImage.Add(new InventoryBitImage(Item1, null));
            ListInvetoryImage.Add(new InventoryBitImage(Item2, null));
            ListInvetoryImage.Add(new InventoryBitImage(Item3, null));
            ListInvetoryImage.Add(new InventoryBitImage(Item4, null));
            ListInvetoryImage.Add(new InventoryBitImage(Item5, null));
            ListInvetoryImage.Add(new InventoryBitImage(Item6, null));

        }

        private void Player_UpLevel(object sender, EventArgs args)
        {
            Player.MaxHealth += 50;
            Player.MaxMana += 50;
            Player.CurrentHP += 50;
            Player.CurrentMana += 50;
            Player.Damage += 2;

            //Tirei o content dialog porque tava bugando
        }

        // Método para configuração e inicialização do timer da animação
        private void StartAnimation() 
        {
            if (!timer.IsEnabled) //O timer só iniciará se ele estiver desligado
            {
                timer.Tick += AnimationEvent;
                timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
                timer.Start();

            }

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ControllerGame.CheckLastPage(typeof(SelecClass), this))
            {
                Player = e.Parameter as Character;
                Person1.Source = Player.IdleRight;
                MobAndChar.Clear();
                PablloVittar = new PablloVittar();
                Ninja = new Ninja();
                MobAndChar.Add(Player);

            }

            if (ControllerGame.CheckLastPage(typeof(BattleScreen), this))
            {
                MobAndChar = e.Parameter as List<Object>;

                Player = MobAndChar.ElementAt(0) as Character;

                Player.UpLevel += Player_UpLevel;

                if (MobAndChar.ElementAt(1) is Ninja) Ninja = MobAndChar.ElementAt(1) as Ninja;
                else if (MobAndChar.ElementAt(1) is PablloVittar) PablloVittar = MobAndChar.ElementAt(1) as PablloVittar;
                MobAndChar.RemoveAt(MobAndChar.Count - 1);
                SoundController.PlaySound(VoiceSound, "CaioInimigosAFrente.mp4");

            }

            if (ControllerGame.CheckLastPage(typeof(Map), this))
            {
                Player = e.Parameter as Character;
                Person1.Source = Player.IdleRight;
                MobAndChar.Clear();
                MobAndChar.Add(Player);

            }

            ControllerGame.RefreshItems(Player, qt_lifePot, qt_manaPot, ListInvetoryImage);
            IsAnotherPage = false;
        }

        //Método que trata toda a animação, movimento e colisão do jogo
        private void AnimationEvent(object sender, object e)
        {
            if (IsAnotherPage) return;

            PosY = Canvas.GetTop(Person1); //Armazena a posição Y do personagem em uma variavel
            PosX = Canvas.GetLeft(Person1); //Armazena a posição X do personagem em uma variavel

            if (ControllerGame.IsMovimentAllowed(Player, Person1, LockedChests, Enemies, Collision, DroppedKeys))
            {
                if (YSpeed < 0 && PosY > 115 * HeightRatio)  //Movimento, checagem e animação para cima
                {
                    ControllerGame.MovePlayer(Person1, 0, YSpeed);
                    ControllerGame.PaintAnimation(Person1, Player, Right, Left, Up, Down);
                }

                if (YSpeed > 0 && PosY < 455 * HeightRatio) //Movimento, checagem e animação para baixo
                {
                    ControllerGame.MovePlayer(Person1, 0, YSpeed);
                    ControllerGame.PaintAnimation(Person1, Player, Right, Left, Up, Down);
                }

                if (XSpeed < 0 && PosX > 60 * WidthRatio) //Movimento, checagem e animação para esquerda
                {
                    ControllerGame.MovePlayer(Person1, XSpeed, 0);
                    ControllerGame.PaintAnimation(Person1, Player, Right, Left, Up, Down);
                }

                if (XSpeed > 0 && PosX < 690 * WidthRatio) //Movimento, checagem e animação para direita
                {
                    ControllerGame.MovePlayer(Person1, XSpeed, 0);
                    ControllerGame.PaintAnimation(Person1, Player, Right, Left, Up, Down);
                }
            }

            //Checa se o player está na frente do bau, para poder lootear
            else if (ControllerGame.CheckListCollision(Player, Person1, LockedChests))
            {
                foreach (Image Hit in LockedChests)
                {
                    if (ControllerGame.Collision(Person1, Hit) == "Bottom")
                    {
                        if (!ChestControl.isOpen)
                        {
                            ControllerGame.LootVault(Player, ChestControl, qt_lifePot, qt_manaPot, ListInvetoryImage);
                            open_chest.Source = new BitmapImage(new Uri(@"ms-appx:///Assets/open_chest3.png"));
                        }

                    }
                }
            }

            else if (ControllerGame.CheckListCollision(Player, Person1, DroppedKeys))
            {
                foreach (Image Hit in DroppedKeys)
                {
                    _Canvas.Children.Remove(FirstMapKey);
                    //Key _Key = new Key();
                    //Player.inventory.Add_Item(_Key, Player);
                }
                DroppedKeys.Clear();
            }

            //Checar se o personagem encontra o Ninja
            if (ControllerGame.CheckCollision(Player, Person1, Enemies.Find(x => x.Name == "NinjaIMG")) && !Ninja.IsDead())
            {
                if (MobAndChar.Count >= 2) MobAndChar.RemoveAt(MobAndChar.Count - 1);
                StoreChars(Ninja as Mob);
                IsAnotherPage = true;
                this.Frame.Navigate(typeof(BattleScreen), MobAndChar);

            }

            //Checar se o personagem encontra o Pabblo
            if (ControllerGame.CheckCollision(Player, Person1, Enemies.Find(x => x.Name == "PabbloVitarIMG")) && !PablloVittar.IsDead())
            {
                if (MobAndChar.Count >= 2) MobAndChar.RemoveAt(MobAndChar.Count - 1);
                StoreChars(PablloVittar as Mob);
                IsAnotherPage = true;
                this.Frame.Navigate(typeof(BattleScreen), MobAndChar);
            }


            else if (ControllerGame.CheckCollision(Player, Person1, Collision.Find(x => x.Name == "MapExit")))
            {
                if (/*Ninja.IsDead() && PablloVittar.IsDead())*/true)  { this.Frame.Navigate(typeof(Map2), Player); }
            }

        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            if (IsAnotherPage) return;

            //Detecta qual direção o personagem irá ir
            switch (args.VirtualKey)
            {
                case Windows.System.VirtualKey.W:
                case Windows.System.VirtualKey.Up:
                    Up = true;
                    YSpeed = -Player.Speed;
                    break;
                case Windows.System.VirtualKey.S:
                case Windows.System.VirtualKey.Down:
                    Down = true;
                    YSpeed = Player.Speed;
                    break;
                case Windows.System.VirtualKey.A:
                case Windows.System.VirtualKey.Left:
                    Left = true;
                    XSpeed = -Player.Speed;
                    break;
                case Windows.System.VirtualKey.Right:
                case Windows.System.VirtualKey.D:
                    Right = true;
                    XSpeed = Player.Speed;
                    break;
            }

        }

        private void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            //Método para checar quando a tecla é levantada, e cancelar a animação que acontecia
            switch (args.VirtualKey)
            {
                case Windows.System.VirtualKey.W:
                case Windows.System.VirtualKey.Up:
                    Person1.Source = Player.IdleUp;
                    Up = false;
                    YSpeed = 0;
                    break;
                case Windows.System.VirtualKey.S:
                case Windows.System.VirtualKey.Down:
                    Person1.Source = Player.IdleDown;
                    Down = false;
                    YSpeed = 0;
                    break;
                case Windows.System.VirtualKey.A:
                case Windows.System.VirtualKey.Left:
                    Person1.Source = Player.IdleLeft;
                    Left = false;
                    XSpeed = 0;
                    break;
                case Windows.System.VirtualKey.D:
                case Windows.System.VirtualKey.Right:
                    Person1.Source = Player.IdleRight;
                    Right = false;
                    XSpeed = 0;
                    break;
            }
        }

        private void Btn_close_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Application.Current.Exit();//fecha o programa
        }

        //Configurações de todos os flyouts daqui pra baixo

        private void ShowStatus(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            status.Text =
            Player.CurrentHP.ToString() + "/" + Player.MaxHealth.ToString() + " HP" + "\n" +
            Player.CurrentMana.ToString() + "/" + Player.MaxMana.ToString() + " MP" + "\n" +
            Player.Damage.ToString() + " Damage" + "\n" +
            Player.Lifesteal.ToString() + "% Lifesteal" + "\n" +
            Player.Speed.ToString() + " Movement Speed" + "\n" +
            Player.CurrentArmor.ToString() + " Armor" + "\n" +
            Player.EvasionRate.ToString() + "% Evasion" + "\n" +
            Player.CriticRate.ToString() + "% Critical Chance" + "\n" +
            Player.CurrentXP.ToString() + "/" + Player.MaxXP + " XP" + "\n" +
            "Level " + Player.Level.ToString();
        }

        private void ShowItemStatus1(object sender, TappedRoutedEventArgs e)
        {
            Item actual = Player.inventory.inventoryList[0];

            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            ItemStatus1.Text = actual.ItemName + "\n" + actual.Description;
        }

        private void ShowItemStatus2(object sender, TappedRoutedEventArgs e)
        {
            Item actual = Player.inventory.inventoryList[1];

            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            ItemStatus2.Text = actual.ItemName + "\n" + actual.Description;
        }

        private void ShowItemStatus3(object sender, TappedRoutedEventArgs e)
        {
            Item actual = Player.inventory.inventoryList[2];

            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            ItemStatus3.Text = actual.ItemName + "\n" + actual.Description;
        }

        private void ShowItemStatus4(object sender, TappedRoutedEventArgs e)
        {
            Item actual = Player.inventory.inventoryList[3];

            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            ItemStatus4.Text = actual.ItemName + "\n" + actual.Description;
        }

        private void ShowItemStatus5(object sender, TappedRoutedEventArgs e)
        {
            Item actual = Player.inventory.inventoryList[4];

            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            ItemStatus5.Text = actual.ItemName + "\n" + actual.Description;
        }

        private void ShowItemStatus6(object sender, TappedRoutedEventArgs e)
        {
            Item actual = Player.inventory.inventoryList[5];

            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            ItemStatus6.Text = actual.ItemName + "\n" + actual.Description;
        }

    }
}
