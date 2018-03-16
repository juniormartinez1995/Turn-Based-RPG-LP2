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

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace RPG_LP2
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class Map : Page
    {
        public Map()
        {
            this.InitializeComponent();

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
            ApplicationView.PreferredLaunchViewSize = new Size(800, 600);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            SetCollision(); //Inicialização das colisões pré-definidas
            SetChetsInMap(); //Inicialização dos baús pré-definidos
            StartAnimation();
            AddImageOnList(); //Inicializa as imagens do Inventário do mapa em um List

            Generator.ChestPopulate(ChestControl); //Método para gerar os itens randomicamente dentro do baú
        }


        double PosY, PosX; //Posição X e Y do personagem no mapa
        bool IsKeyPressed, Up , Down, Right, Left; //Checagem da direção que o personagem está indo
        int Velocity = 3; //Velocidade do personagem
        
        DispatcherTimer timer = new DispatcherTimer(); //Timer da animação
        List<Image> Collision = new List<Image>(); //Lista de colisões no mapa
        List<Image> LockedChests = new List<Image>(); //Lista de baús no mapa
        List<BitmapImage> InventoryImage = new List<BitmapImage>(); //Lista das Imagens de Inventário
        List<Image> InventoryMap = new List<Image>();
        Character player; //Personagem que estará no mapa
        Chest ChestControl = new Chest(); //Gerenciamento do baú

        //Método para setar os baús no mapa
        public void SetChetsInMap()
        {
            LockedChests.Add(Chest0);
        }

        //Método para setar as colisões no mapa
        public void SetCollision()
        {
            Collision.Add(Collision0);
            Collision.Add(Collision1);
            Collision.Add(Collision2);
            Collision.Add(Collision3);

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Berserker p1 = (Berserker)e.Parameter;
            player = p1;

        }

        private void AddImageOnList() 
        {
            InventoryMap.Add(Item1);
            InventoryMap.Add(Item2);
            InventoryMap.Add(Item3);
            InventoryMap.Add(Item4);
            InventoryMap.Add(Item5);
            InventoryMap.Add(Item6);

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
            PosY = Canvas.GetTop(Person1); //Armazena a posição Y do personagem em uma variavel
            PosX = Canvas.GetLeft(Person1); //Armazena a posição X do personagem em uma variavel

            if (Up && PosY > 140 && !ControllerGame.IsPlayerOverChest(Person1, LockedChests, Up))  //Movimento, checagem e animação para cima
            {
                if (!ControllerGame.IsPlayerColliding(Person1, Collision, Up))
                {
                    ControllerGame.MoveUp(Person1, Velocity);
                    ControllerGame.PaintAnimation(Person1, player, Right, Left, Up, Down);
                }
                else PosY += Velocity * 3;
            }
            if (Down && PosY < 470 && !ControllerGame.IsPlayerOverChest(Person1, LockedChests ,Down)) //Movimento, checagem e animação para baixo
            {

                if (!ControllerGame.IsPlayerColliding(Person1, Collision, Down))
                {
                    ControllerGame.MoveDown(Person1, Velocity);
                    ControllerGame.PaintAnimation(Person1, player, Right, Left, Up, Down);
                }
                else PosY -= Velocity * 2;
            }
            if (Left && PosX > 70 && !ControllerGame.IsPlayerOverChest(Person1, LockedChests, Left)) //Movimento, checagem e animação para esquerda
            {
                if (!ControllerGame.IsPlayerColliding(Person1, Collision, Left))
                {
                    ControllerGame.MoveLeft(Person1, Velocity);
                    ControllerGame.PaintAnimation(Person1, player, Right, Left, Up, Down);
                }
                else PosX += Velocity * 2;
            }
            if (Right && PosX < 690 && !ControllerGame.IsPlayerOverChest(Person1, LockedChests, Right)) //Movimento, checagem e animação para direita
            {
                if (!ControllerGame.IsPlayerColliding(Person1, Collision, Right))
                {
                    ControllerGame.MoveRight(Person1, Velocity);
                    ControllerGame.PaintAnimation(Person1, player, Right, Left, Up, Down);
                }
                else PosX -= Velocity * 2;
            }
            else if (ControllerGame.IsPlayerOverChest(Person1, LockedChests, Up)) //CORE
            {
                ControllerGame.LootVault(player, ChestControl, qt_lifePot, qt_manaPot, InventoryImage, InventoryMap);
                
            }
        }


        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {

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
                    Person1.Source = player.IdleUp;
                    ControllerGame.MoveDown(Person1, Velocity);
                    Up = false;
                    break;
                case Windows.System.VirtualKey.Down:
                    Person1.Source = player.IdleDown;
                    ControllerGame.MoveUp(Person1 ,Velocity);
                    Down = false;
                    break;
                case Windows.System.VirtualKey.Left:
                    Person1.Source = player.IdleLeft;
                    ControllerGame.MoveRight(Person1, Velocity);
                    Left = false;
                    break;
                case Windows.System.VirtualKey.Right:
                    Person1.Source = player.IdleRight;
                    ControllerGame.MoveLeft(Person1 , Velocity);
                    Right = false;
                    break;
            }
            IsKeyPressed = false;

        }

    }
}
