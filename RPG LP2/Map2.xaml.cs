﻿using RPGlib.Characters;
using RPGlib.Itens;
using RPGlib.Mobs;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace RPG_LP2
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class Map2 : Page
    {
        public Map2()
        {
            this.InitializeComponent();
            ControllerGame.AdjustFullScreenMode(_Canvas, this);
            this.NavigationCacheMode = NavigationCacheMode.Enabled;

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
            StartAnimation();
            SetCollision();
            SetEnemies();
            SetChests();
            AddImageOnList();

            Generator.ChestPopulate(ChestControl);

            WidthRatio = _Canvas.Width / 800;
            HeightRatio = _Canvas.Height / 600;

            //Mob PablloVittar, Ninja, Salamander, Mouse, SecondMouse, Mimic;
            PablloVittar = Creator.CreateMob("PablloVittar");
            Ninja = Creator.CreateMob("Ninja");
            Salamander_ = Creator.CreateMob("Salamander");
            Mouse = Creator.CreateMob("Mouse");
            SecondMouse = Creator.CreateMob("Mouse");
            Mimic = Creator.CreateMob("Mimic");

        }

        DispatcherTimer timer = new DispatcherTimer(); //Timer da animação
        List<Image> Collision = new List<Image>(); //Lista de colisões no mapa
        List<Image> LockedChests = new List<Image>(); //Lista de baús no mapa
        List<Image> Enemies = new List<Image>(); //Lista de enimigos no mapa
        List<Image> Keys = new List<Image>(); //Lista de chaves no mapa
        List<Image> DroppedKeys = new List<Image>();
        List<object> MobAndChar = new List<object>();
        List<InventoryBitImage> ListInvetoryImage = new List<InventoryBitImage>(); //Lista das Imagens de Inventário


        CharacterFactory Creator = new CharacterFactory();
        Character Player; //Personagem que estará no mapa
        Mob PablloVittar, Ninja, Salamander_, Mouse, SecondMouse, Mimic;
        Chest ChestControl = new Chest(); //Gerenciamento do baú

        double WidthRatio, HeightRatio;
        double PosY, PosX, XSpeed, YSpeed; //Posição X e Y do personagem no mapa
        bool Up, Down, Right, Left, IsAnotherPage; //Checagem da direção que o personagem está indo


        public void StartAnimation()
        {
            if (!timer.IsEnabled) //O timer só iniciará se ele estiver desligado
            {
                timer.Tick += AnimationEvent;
                timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
                timer.Start();

            }
        }

        public void StoreChars(Mob EnemyMob)
        {
            MobAndChar.Add(EnemyMob);
        }

        public void SetCollision()
        {
            Collision.Add(Collision0);
            Collision.Add(Collision1);
            Collision.Add(MapExit);
            Collision.Add(MapExit1);
            Collision.Add(MapExit2);
        }

        public void SetEnemies()
        {
            Enemies.Add(OpenedChest);
            Enemies.Add(Salamander);
            Enemies.Add(FirstRat);
            Enemies.Add(SecondRat);
        }

        public void SetChests()
        {
            LockedChests.Add(LockedChest);
        }

        private void AddImageOnList()
        {
            ListInvetoryImage.Add(new InventoryBitImage(Item1, null));
            ListInvetoryImage.Add(new InventoryBitImage(Item2, null));
            ListInvetoryImage.Add(new InventoryBitImage(Item3, null));
            ListInvetoryImage.Add(new InventoryBitImage(Item4, null));
            ListInvetoryImage.Add(new InventoryBitImage(Item5, null));
            ListInvetoryImage.Add(new InventoryBitImage(Item6, null));

        }

        private void AnimationEvent(object sender, object e)
        {
            if (IsAnotherPage) return;

            PosY = Canvas.GetTop(Person1); //Armazena a posição Y do personagem em uma variavel
            PosX = Canvas.GetLeft(Person1); //Armazena a posição X do personagem em uma variavel

            if (ControllerGame.IsMovimentAllowed(Player, Person1, LockedChests, Enemies, Collision, Keys))
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

            if (ControllerGame.CheckCollision(Player, Person1, Enemies.Find(x => x.Name == "OpenedChest")) && !Mimic.IsDead())
            {
                if (MobAndChar.Count >= 2) MobAndChar.RemoveAt(MobAndChar.Count - 1);
                StoreChars(Mimic as Mob);
                IsAnotherPage = true;
                this.Frame.Navigate(typeof(BattleScreen), MobAndChar);

            }

            if (ControllerGame.CheckCollision(Player, Person1, Enemies.Find(x => x.Name == "Salamander")) && !Salamander_.IsDead())
            {
                if (MobAndChar.Count >= 2) MobAndChar.RemoveAt(MobAndChar.Count - 1);
                StoreChars(Salamander_ as Mob);
                IsAnotherPage = true;
                this.Frame.Navigate(typeof(BattleScreen), MobAndChar);

            }

            //Checar se o personagem encontra o Pabblo
            if (ControllerGame.CheckCollision(Player, Person1, Enemies.Find(x => x.Name == "FirstRat")) && !Mouse.IsDead())
            {
                if (MobAndChar.Count >= 2) MobAndChar.RemoveAt(MobAndChar.Count - 1);
                StoreChars(Mouse as Mob);
                IsAnotherPage = true;
                this.Frame.Navigate(typeof(BattleScreen), MobAndChar);
            }

            if (ControllerGame.CheckCollision(Player, Person1, Enemies.Find(x => x.Name == "SecondRat")) && !SecondMouse.IsDead())
            {
                if (MobAndChar.Count >= 2) MobAndChar.RemoveAt(MobAndChar.Count - 1);
                StoreChars(SecondMouse as Mob);
                IsAnotherPage = true;
                this.Frame.Navigate(typeof(BattleScreen), MobAndChar);
            }


            else if (ControllerGame.CheckCollision(Player, Person1, Collision.Find(x => x.Name == "MapExit")))
            {
                this.Frame.Navigate(typeof(Map), Player);


            }

            else if ((ControllerGame.CheckCollision(Player, Person1, Collision.Find(x => x.Name == "MapExit1")) || ControllerGame.CheckCollision(Player, Person1, Collision.Find(x => x.Name == "MapExit2"))) &&
                Mouse.IsDead() && SecondMouse.IsDead() && Salamander_.IsDead())
            {
                if (MobAndChar.Count >= 2) MobAndChar.RemoveAt(MobAndChar.Count - 1);
                StoreChars(PablloVittar as Mob);
                IsAnotherPage = true;
                this.Frame.Navigate(typeof(BattleScreen), MobAndChar);


            }

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

        private void btn_close_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Application.Current.Exit();//fecha o programa
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ControllerGame.CheckLastPage(typeof(Map), this))
            {
                Player = e.Parameter as Character;
                Person1.Source = Player.IdleRight;
                MobAndChar.Add(Player);

                ControllerGame.RefreshItems(Player, qt_lifePot, qt_manaPot, ListInvetoryImage);

            }

            if (ControllerGame.CheckLastPage(typeof(BattleScreen), this))
            {
                MobAndChar = e.Parameter as List<Object>;
                Player = MobAndChar.ElementAt(0) as Character;
                ControllerGame.RefreshItems(Player, qt_lifePot, qt_manaPot, ListInvetoryImage);
                Player.UpLevel += Player_UpLevel;

                if (MobAndChar.ElementAt(1) is Ninja) Ninja = MobAndChar.ElementAt(1) as Ninja;
                else if (MobAndChar.ElementAt(1) is PablloVittar) PablloVittar = MobAndChar.ElementAt(1) as PablloVittar;
                MobAndChar.RemoveAt(MobAndChar.Count - 1);
                //ControllerGame.PlaySoundsVitorHugo("CaioInimigosAFrente.mp4");

            }
            IsAnotherPage = false;
        }


        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            if (IsAnotherPage) return;

            switch (args.VirtualKey) //Detecta qual direção o personagem irá ir
            {
                case Windows.System.VirtualKey.Up:
                    Up = true;
                    YSpeed = -Player.Speed;
                    break;
                case Windows.System.VirtualKey.Down:
                    Down = true;
                    YSpeed = Player.Speed;
                    break;
                case Windows.System.VirtualKey.Left:
                    Left = true;
                    XSpeed = -Player.Speed;
                    break;
                case Windows.System.VirtualKey.Right:
                    Right = true;
                    XSpeed = Player.Speed;
                    break;
            }
        }

        private void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            //Método para checar quando a tecla é levantada, e cancelar a animação que acontecia
            switch (args.VirtualKey)
            {
                case Windows.System.VirtualKey.Up:
                    Person1.Source = Player.IdleUp;
                    Up = false;
                    YSpeed = 0;
                    break;
                case Windows.System.VirtualKey.Down:
                    Person1.Source = Player.IdleDown;
                    Down = false;
                    YSpeed = 0;
                    break;

                case Windows.System.VirtualKey.Left:
                    Person1.Source = Player.IdleLeft;
                    Left = false;
                    XSpeed = 0;
                    break;

                case Windows.System.VirtualKey.Right:
                    Person1.Source = Player.IdleRight;
                    Right = false;
                    XSpeed = 0;
                    break;
            }
        }

        //Putaria pura daqui pra baixo -------

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
