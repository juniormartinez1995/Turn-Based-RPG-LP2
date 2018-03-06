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
        }

        
        double PosY, PosX;
        bool Up, Down, Right, Left, Contador = true;
        int Aux = 1;

        DispatcherTimer timer = new DispatcherTimer();
        BitmapImage[] upMoviment = new BitmapImage[] {
            new BitmapImage(new Uri(@"ms-appx:///Assets/upAnimation/0.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/upAnimation/1.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/upAnimation/2.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/upAnimation/3.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/upAnimation/4.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/upAnimation/5.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/upAnimation/6.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/upAnimation/7.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/upAnimation/8.png"))

        }; // Imagens para o movimento para cima
        BitmapImage[] downMoviment = new BitmapImage[] {
            new BitmapImage(new Uri(@"ms-appx:///Assets/downAnimation/0.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/downAnimation/1.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/downAnimation/2.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/downAnimation/3.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/downAnimation/4.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/downAnimation/5.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/downAnimation/6.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/downAnimation/7.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/downAnimation/8.png"))

        };  // Imagens para o movimento para baixo
        BitmapImage[] rightMoviment = new BitmapImage[] {
            new BitmapImage(new Uri(@"ms-appx:///Assets/rightAnimation/0.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/rightAnimation/1.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/rightAnimation/2.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/rightAnimation/3.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/rightAnimation/4.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/rightAnimation/5.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/rightAnimation/6.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/rightAnimation/7.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/rightAnimation/8.png"))

        };  // Imagens para o movimento para direita
        BitmapImage[] leftMoviment = new BitmapImage[] {
            new BitmapImage(new Uri(@"ms-appx:///Assets/leftAnimation/0.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/leftAnimation/1.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/leftAnimation/2.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/leftAnimation/3.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/leftAnimation/4.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/leftAnimation/5.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/leftAnimation/6.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/leftAnimation/7.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/leftAnimation/8.png"))

        };  // Imagens para o movimento para esquerda

        private void StartAnimation() // Método para configuração e inicialização do timer da animação
        {
            if (!timer.IsEnabled)
            {
                timer.Tick += AnimationEvent;
                timer.Interval = new TimeSpan(0, 0, 0, 0, 110);
                timer.Start();
            }

        }

        private void StopAnimation() //Método para parar a animação do movimento
        {
            timer.Stop();
        }


        private void AnimationEvent(object sender, object e) //Timer que roda o codigo escrito
        {                                                    // A cada 110 milisegundos       

            if (Up)
            {
                PaintAnimation(upMoviment);
            }
            if (Down)
            {
                PaintAnimation(downMoviment);
            }
            if (Left)
            {
                PaintAnimation(leftMoviment);
            }
            if (Right)
            {
                PaintAnimation(rightMoviment);
            }
        }

        public void PaintAnimation(BitmapImage[] MovimentArray) // Método de recebe como parametro um vetor de Bitmap
        {                                                       // e realiza a animação do movimento
            if (Aux < 9)
            {
                Person1.Source = MovimentArray[Aux];
                Aux++;
            }
            else
            {
                Person1.Source = MovimentArray[1];
                Aux = 1;
            }
        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            PosY = Canvas.GetTop(Person1); //Armazena a posição Y do personagem em uma variavel
            PosX = Canvas.GetLeft(Person1); //Armazena a posição X do personagem em uma variavel
            
            if (args.VirtualKey == Windows.System.VirtualKey.Up && PosY > 0) 
            {
                Up = true;
                MoveUp();
                StartAnimation();


            }
            if (args.VirtualKey == Windows.System.VirtualKey.Down && PosY < 570)
            {
                Down = true;
                MoveDown();
                StartAnimation();

            }
            if (args.VirtualKey == Windows.System.VirtualKey.Right && PosX < 770)
            {
                Right = true;
                MoveRight();
                StartAnimation();

            }
            if (args.VirtualKey == Windows.System.VirtualKey.Left && PosX > 0)
            {
                Left = true;
                MoveLeft();
                StartAnimation();

            }
        }


        private void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            if (Up)
            {
                StopAnimation();
                Person1.Source = upMoviment[0];
                Up = false;

            }
            if (Down)
            {
                StopAnimation();
                Person1.Source = downMoviment[0];
                Down = false;
            }
            if (Left)
            {
                StopAnimation();
                Person1.Source = leftMoviment[0];
                Left = false;
            }
            if (Right)
            {
                StopAnimation();
                Person1.Source = rightMoviment[0];
                Right = false;
            }
        }

        private void MoveUp() //Método que realiza a movimentação da imagem para cima
        {
            Person1.SetValue(Canvas.TopProperty, PosY - 2);
        }

        private void MoveDown() //Método que realiza a movimentação da imagem para baixo
        {
            Person1.SetValue(Canvas.TopProperty, PosY + 2);
        }

        private void MoveLeft() //Método que realiza a movimentação da imagem para esquerda
        {
            Person1.SetValue(Canvas.LeftProperty, PosX - 2);
        }

        private void MoveRight() //Método que realiza a movimentação da imagem para direita
        {
            Person1.SetValue(Canvas.LeftProperty, PosX + 2);
        }

    }
}
