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
    public sealed partial class BlankPage1 : Page
    {
        public BlankPage1()
        {
            this.InitializeComponent();
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
        }

        double PosY, PosX;
        bool up, down, right, left;

        BitmapImage test1 = new BitmapImage(new Uri(@"ms-appx:///Assets/StoreLogo.png"));

        private void startTimer1() // Método para configuração e inicialização do timer da animação
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 180);
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            //Animação do personagem ficará aqui
        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            PosY = Canvas.GetTop(Person1); //Armazena a posição Y do personagem em uma variavel
            PosX = Canvas.GetLeft(Person1); //Armazena a posição X do personagem em uma variavel

            if (args.VirtualKey == Windows.System.VirtualKey.Up)
            {
                moveUp();
                up = true;

                Person1.Source = test1;
            }
            if (args.VirtualKey == Windows.System.VirtualKey.Down)
            {
                moveDown();
                down = true;
            }
            if (args.VirtualKey == Windows.System.VirtualKey.Right)
            {
                moveRight();
                right = true;
            }
            if (args.VirtualKey == Windows.System.VirtualKey.Left)
            {
                moveLeft();
                left = true;
            }
        }


        private void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            if (up)
            {
                up = false;
            }
            if (down)
            {
                down = false;
            }
            if (left)
            {
                left = false;
            }
            if (right)
            {
                right = false;
            }
        }

        private void moveUp() //Método que realiza a movimentação da imagem para cima
        {
            Person1.SetValue(Canvas.TopProperty, PosY - 5);
        }

        private void moveDown() //Método que realiza a movimentação da imagem para baixo
        {
            Person1.SetValue(Canvas.TopProperty, PosY + 5);
        }

        private void moveLeft() //Método que realiza a movimentação da imagem para esquerda
        {
            Person1.SetValue(Canvas.LeftProperty, PosX - 5);
        }

        private void moveRight() //Método que realiza a movimentação da imagem para direita
        {
            Person1.SetValue(Canvas.LeftProperty, PosX + 5);
        }

    }
}
