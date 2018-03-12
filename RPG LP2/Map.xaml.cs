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

            Generator.ChestPopulate(ChestControl); //Método para gerar os itens randomicamente dentro do baú
        }


        double PosY, PosX; //Posição X e Y do personagem no mapa
        bool IsKeyPressed, Up, Down, Right, Left; //Checagem da direção que o personagem está indo
        int Velocity = 4; //Velocidade do personagem
        
        DispatcherTimer timer = new DispatcherTimer(); //Timer da animação
        List<Image> Collision = new List<Image>(); //Lista de colisões no mapa
        List<Image> LockedChests = new List<Image>(); //Lista de baús no mapa
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

        public bool IsPlayerOverChest(bool key) //Checa se o personagem encontrou um baú no mapa
        {
            foreach(Image vault in LockedChests)
            {
                if (IsPlayerOverItem(LockedChests, key, LockedChests.Count)) return true;
            }

            return false;
        }

        public bool IsPlayerColliding(bool key) //Checa se o personagem colide com algum objeto e/ou personagem
        {
            for (int i = 0; i < Collision.Count; i++)
            {
                if (IsPlayerOverItem(Collision, key, i)) return true;
            }
            return false;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Berserker p1 = (Berserker)e.Parameter;
            player = p1;

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

            if (Up && PosY > 140)  //Movimento, checagem e animação para cima
            {
                if (!IsPlayerColliding(Up))
                {
                    MoveUp();
                    PaintAnimation(player);
                }
                else PosY += Velocity * 3;
            }
            if (Down && PosY < 470) //Movimento, checagem e animação para baixo
            {

                if (!IsPlayerColliding(Down))
                {
                    MoveDown();
                    PaintAnimation(player);
                }
                else PosY -= Velocity * 2;
            }
            if (Left && PosX > 70) //Movimento, checagem e animação para esquerda
            {
                if (!IsPlayerColliding(Left))
                {
                    MoveLeft();
                    PaintAnimation(player);
                }
                else PosX += Velocity * 2;
            }
            if (Right && PosX < 690) //Movimento, checagem e animação para direita
            {
                if (!IsPlayerColliding(Right))
                {
                    MoveRight();
                    PaintAnimation(player);
                }
                else PosX -= Velocity * 2;
            }
            else if (IsPlayerOverChest(Up)) Application.Current.Exit();
        }

        public void PaintAnimation(Character Person) // Método de recebe como parametro um vetor de Bitmap
        {                                                       // e realiza a animação do movimento
            if (Right) Person1.Source = Person.RightMoviment;
            if (Left) Person1.Source = Person.LeftMoviment;
            if (Up) Person1.Source = Person.UpMoviment;
            if (Down) Person1.Source = Person.DownMoviment;
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            PosY = Canvas.GetTop(Person1); //Armazena a posição Y do personagem em uma variavel
            PosX = Canvas.GetLeft(Person1); //Armazena a posição X do personagem em uma variavel

            Item.Source = player.IdleDown; //Posição padrao do personagem

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

            StartAnimation();
        }


        private void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            //Método para checar quando a tecla é levantada, e cancelar a animação que acontecia
            switch (args.VirtualKey)
            {
                case Windows.System.VirtualKey.Up:
                    Person1.Source = player.IdleUp;
                    MoveDown(Velocity);
                    Up = false;
                    break;
                case Windows.System.VirtualKey.Down:
                    Person1.Source = player.IdleDown;
                    MoveUp(Velocity);
                    Down = false;
                    break;
                case Windows.System.VirtualKey.Left:
                    Person1.Source = player.IdleLeft;
                    MoveRight(Velocity);
                    Left = false;
                    break;
                case Windows.System.VirtualKey.Right:
                    Person1.Source = player.IdleRight;
                    MoveLeft(Velocity);
                    Right = false;
                    break;
            }
            timer.Stop();
            IsKeyPressed = false;
        }

        //Método geral para checar se o personagem está sobre qualquer objeto
        public bool IsPlayerOverItem(List<Image> _item, bool key, int j)
        {

            if (PosX + Person1.Width >= Canvas.GetLeft(_item[j]) &&
                PosX <= Canvas.GetLeft(_item[j]) + _item[j].Width &&
                PosY + Person1.Height >= Canvas.GetTop(_item[j]) &&
                PosY <= Canvas.GetTop(_item[j]) + _item[j].Height
                )
            {
                if (key) return true;
                else return false;

            }
            else return false;
        }

        private void MoveUp(int Increment = 0) //Método que realiza a movimentação da imagem para cima
        {
            Person1.SetValue(Canvas.TopProperty, PosY - Velocity + Increment);
        }

        private void MoveDown(int Increment = 0) //Método que realiza a movimentação da imagem para baixo
        {
            Person1.SetValue(Canvas.TopProperty, PosY + Velocity - Increment);
        }

        private void MoveLeft(int Increment = 0) //Método que realiza a movimentação da imagem para esquerda
        {
            Person1.SetValue(Canvas.LeftProperty, PosX - Velocity + Increment);
        }

        private void MoveRight(int Increment = 0) //Método que realiza a movimentação da imagem para direita
        {
            Person1.SetValue(Canvas.LeftProperty, PosX + Velocity - Increment);
        }

    }
}
