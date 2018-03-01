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
        int up = 0, down = 0, right = 0, left = 0, Aux = 0, contador = 0;

        DispatcherTimer timer = new DispatcherTimer();
        BitmapImage[] upMoviment = new BitmapImage[] {
            new BitmapImage(new Uri(@"ms-appx:///Assets/upAnimation/Cima1.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/upAnimation/Cima2.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/upAnimation/Cima3.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/upAnimation/Cima4.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/upAnimation/Cima5.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/upAnimation/Cima6.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/upAnimation/Cima7.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/upAnimation/Cima8.png"))

        };

        private void startAnimation() // Método para configuração e inicialização do timer da animação
        {
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 210);
            timer.Start();
        }

        private void stopAnimation()
        {
            timer.Stop();
        }


        private void Timer_Tick(object sender, object e)
        {

            if (Aux < 8)
            {
                Person1.Source = upMoviment[Aux];
                Aux++;
            }
            else
            {
                Person1.Source = upMoviment[1];
                Aux = 0;
            }
        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            PosY = Canvas.GetTop(Person1); //Armazena a posição Y do personagem em uma variavel
            PosX = Canvas.GetLeft(Person1); //Armazena a posição X do personagem em uma variavel
            
            if (args.VirtualKey == Windows.System.VirtualKey.Up && PosY > 0) 
            {
                moveUp();

                if(contador == 0)
                {
                    startAnimation();
                    contador ++;
                }

                up = 1;
            }
            if (args.VirtualKey == Windows.System.VirtualKey.Down && PosY < 570)
            {
                moveDown();
                down = 1;
            }
            if (args.VirtualKey == Windows.System.VirtualKey.Right && PosX < 770)
            {
                moveRight();
                right = 1;
            }
            if (args.VirtualKey == Windows.System.VirtualKey.Left && PosX > 0)
            {
                moveLeft();
                left = 1;
            }
        }


        private void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            if (up == 1)
            {
                stopAnimation();
                contador --;
                up = 0;

            }
            if (down == 1)
            {
                down = 0;
            }
            if (left == 1)
            {
                left = 0;
            }
            if (right == 1)
            {
                right = 0;
            }
        }

        private void moveUp() //Método que realiza a movimentação da imagem para cima
        {
            Person1.SetValue(Canvas.TopProperty, PosY - 3);
        }

        private void moveDown() //Método que realiza a movimentação da imagem para baixo
        {
            Person1.SetValue(Canvas.TopProperty, PosY + 3);
        }

        private void moveLeft() //Método que realiza a movimentação da imagem para esquerda
        {
            Person1.SetValue(Canvas.LeftProperty, PosX - 3);
        }

        private void moveRight() //Método que realiza a movimentação da imagem para direita
        {
            Person1.SetValue(Canvas.LeftProperty, PosX + 3);
        }

    }
}
