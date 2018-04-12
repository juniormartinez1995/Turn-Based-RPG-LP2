﻿using System;
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


// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace RPG_LP2
{
   
    public sealed partial class Map : Page
    {
        public Map()
        {
            this.InitializeComponent();
           
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
            Window.Current.CoreWindow.KeyDown += ControllerGame.CoreWindow_KeyDown;
            ApplicationView.PreferredLaunchViewSize = new Size(800, 600);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            this.NavigationCacheMode = NavigationCacheMode.Enabled;

            StartAnimation(); //Startar a animação
            SetCollision(); //Inicialização das colisões pré-definidas
            SetChetsInMap(); //Inicialização dos baús pré-definidos
            AddImageOnList(); //Inicializa as imagens do Inventário do mapa em um List
            SetEnemiesPosition(); //Inicializa os inimigos
            Generator.ChestPopulate(ChestControl); //Método para gerar os itens randomicamente dentro do baú
                     
        }

        DispatcherTimer timer = new DispatcherTimer(); //Timer da animação
        List<Image> Collision = new List<Image>(); //Lista de colisões no mapa
        List<Image> LockedChests = new List<Image>(); //Lista de baús no mapa
        List<Image> Enemies = new List<Image>(); //Lista de enimigos no mapa
        
        //MUDEI AQUI, N SEI SE ESTA CERTO
        List<InventoryBitImage> ListInvetoryImage = new List<InventoryBitImage>(); //Lista das Imagens de Inventário
        

        Character Player; //Personagem que estará no mapa
        Chest ChestControl = new Chest(); //Gerenciamento do baú
       


        double PosY, PosX; //Posição X e Y do personagem no mapa
        bool IsKeyPressed, Up, Down, Right, Left, IsAnotherPage; //Checagem da direção que o personagem está indo
        int Velocity = 3; //Velocidade do personagem

      

        //Método para setar os baús no mapa
        public void SetChetsInMap()
        {
            LockedChests.Add(Chest0);
        }

        //Método para setar os inimigos no mapa
        public void SetEnemiesPosition()
        {
            Enemies.Add(Enemy0);
            Enemies.Add(Enemy1);

        }

        //Método para setar as colisões no mapa
        public void SetCollision()
        {
            Collision.Add(Collision0);
            Collision.Add(Collision1);
            Collision.Add(Collision2);
            Collision.Add(Collision3);

        }

        private void ShowItemStatus1(object sender, TappedRoutedEventArgs e) //Flyout para mostrar os status do 1 item do inventário
        {
            Item actual = Player.inventory.inventoryList[0];

            FlyoutBase.ShowAttachedFlyout((FrameworkElement) sender);
            ItemStatus1.Text = actual.ItemName +"\n"+ actual.Description;
        }

        private void ShowItemStatus2(object sender, TappedRoutedEventArgs e)  //Flyout para mostrar os status do 2 item do inventário
        {
            Item actual = Player.inventory.inventoryList[1];

            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            ItemStatus2.Text = actual.ItemName + "\n" + actual.Description;
        }

        private void ShowItemStatus3(object sender, TappedRoutedEventArgs e)  //Flyout para mostrar os status do 3 item do inventário
        {
            Item actual = Player.inventory.inventoryList[2];

            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            ItemStatus3.Text = actual.ItemName + "\n" + actual.Description;
        }

        private void ShowItemStatus4(object sender, TappedRoutedEventArgs e)  //Flyout para mostrar os status do 4 item do inventário
        {
            Item actual = Player.inventory.inventoryList[3];

            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            ItemStatus4.Text = actual.ItemName + "\n" + actual.Description;
        }

        private void ShowItemStatus5(object sender, TappedRoutedEventArgs e)  //Flyout para mostrar os status do 5 item do inventário
        {
            Item actual = Player.inventory.inventoryList[4];

            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            ItemStatus5.Text = actual.ItemName + "\n" + actual.Description;
        }

        private void ShowItemStatus6(object sender, TappedRoutedEventArgs e)  //Flyout para mostrar os status do 6 item do inventário
        {
            Item actual = Player.inventory.inventoryList[5];

            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            ItemStatus6.Text = actual.ItemName + "\n" + actual.Description;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            Player = e.Parameter as Character;
            IsAnotherPage = false;

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

        // Ainda estou implementando essa bagaça aqui

        //private void ButtonStatus_PointerEntered(object sender, PointerRoutedEventArgs e)
        //{
        //    FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        //    Debug.WriteLine("Mouse entrou");
        //}

        //private void ButtonStatus_PointerExited(object sender, PointerRoutedEventArgs e)
        //{
        //    Debug.WriteLine("Mouse saiu");
        //}

        private void ShowStatus(object sender, TappedRoutedEventArgs e)   //Flyout mostrand os status do personagem
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement) sender);
            status.Text =
            "HP = " + Player.CurrentHP.ToString() + "/" + Player.MaxHealth.ToString() + "\n" +
            "MP = " + Player.CurrentMana.ToString() + "/" + Player.MaxMana.ToString() + "\n" +
            "Damage = " + Player.Damage.ToString() + "\n" +
            "Armor = " + Player.CurrentArmor.ToString() + "\n" +
            "Evasion = " + Player.EvasionRate.ToString() + "%" + "\n" +
            "Critical = " + Player.CriticRate.ToString() + "%" + "\n" +
            "XP = " + Player.CurrentXP.ToString() + "/" + Player.MaxXP + "\n" +
            "Level = " + Player.Level.ToString() ;
        }

        

        private void StartAnimation() // Método para configuração e inicialização do timer da animação
        {
            if (!timer.IsEnabled) //O timer só iniciará se ele estiver desligado
            {
                timer.Tick += AnimationEvent;
                timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
                timer.Start();
                
            }

        }
        
        
        private void AnimationEvent(object sender, object e) //Timer que roda o codigo escrito
        {                                                    // A cada 110 milisegundos       
            if (IsAnotherPage) return;

            PosY = Canvas.GetTop(Person1); //Armazena a posição Y do personagem em uma variavel
            PosX = Canvas.GetLeft(Person1); //Armazena a posição X do personagem em uma variavel

            Player.CurrentPosY = PosY;
            Player.CurrentPosX = PosX;

            if (Up && PosY > 140 && ControllerGame.IsMovimentAllowed(Person1, LockedChests, Enemies, Collision, Up))  //Movimento, checagem e animação para cima
            {
                ControllerGame.MoveUp(Person1, Velocity);
                ControllerGame.PaintAnimation(Person1, Player, Right, Left, Up, Down);
            }


            if (Down && PosY < 470 && ControllerGame.IsMovimentAllowed(Person1, LockedChests, Enemies, Collision, Down)) //Movimento, checagem e animação para baixo
            {
                ControllerGame.MoveDown(Person1, Velocity);
                ControllerGame.PaintAnimation(Person1, Player, Right, Left, Up, Down);
            }


            if (Left && PosX > 70 && ControllerGame.IsMovimentAllowed(Person1, LockedChests, Enemies, Collision, Left)) //Movimento, checagem e animação para esquerda
            {
                ControllerGame.MoveLeft(Person1, Velocity);
                ControllerGame.PaintAnimation(Person1, Player, Right, Left, Up, Down);
            }


            if (Right && PosX < 690 && ControllerGame.IsMovimentAllowed(Person1, LockedChests, Enemies, Collision, Right)) //Movimento, checagem e animação para direita
            {
                ControllerGame.MoveRight(Person1, Velocity);
                ControllerGame.PaintAnimation(Person1, Player, Right, Left, Up, Down);
            }

            //Checa se o player está na frente do bau, para poder lootear
            else if (ControllerGame.IsPlayerOverChest(Person1, LockedChests, Up))
            {
                ControllerGame.LootVault(Player, ChestControl, qt_lifePot, qt_manaPot, ListInvetoryImage);

            }
            //Checa se o player se encontra de frente com o mob, se sim, iniciará a tela de batalha
            else if (ControllerGame.IsPlayerColliding(Person1, Enemies, Up))
            {
               
                IsAnotherPage = true;
                this.Frame.Navigate(typeof(BattleScreen), Player); //Irá para a tela de batalha
             
            }
        }
       


        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            if (IsAnotherPage) return;

            if (!IsKeyPressed)
            {

                switch (args.VirtualKey) //Detecta qual direção o personagem irá ir
                {
                    case Windows.System.VirtualKey.Up:
                        Up = true;
                        break;
                    case Windows.System.VirtualKey.Down:
                        Down = true;
                        break;
                    case Windows.System.VirtualKey.Left:
                        Left = true;
                        break;
                    case Windows.System.VirtualKey.Right:
                        Right = true;
                        break;
                }

                IsKeyPressed = true;
            }

        }


        private void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            //Método para checar quando a tecla é levantada, e cancelar a animação que acontecia
            switch (args.VirtualKey)
            {
                case Windows.System.VirtualKey.Up:
                    Person1.Source = Player.IdleUp;
                    ControllerGame.MoveDown(Person1, Velocity);
                    Up = false;
                    break;
                case Windows.System.VirtualKey.Down:
                    Person1.Source = Player.IdleDown;
                    ControllerGame.MoveUp(Person1, Velocity);
                    Down = false;
                    break;
                case Windows.System.VirtualKey.Left:
                    Person1.Source = Player.IdleLeft;
                    ControllerGame.MoveRight(Person1, Velocity);
                    Left = false;
                    break;
                case Windows.System.VirtualKey.Right:
                    Person1.Source = Player.IdleRight;
                    ControllerGame.MoveLeft(Person1, Velocity);
                    Right = false;
                    break;
            }
            IsKeyPressed = false;

        }

    }
}
