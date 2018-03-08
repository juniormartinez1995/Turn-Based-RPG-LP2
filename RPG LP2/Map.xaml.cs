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
        bool Up, Down, Right, Left;
        int Velocity = 3;

        DispatcherTimer timer = new DispatcherTimer();
        BitmapImage[] upMoviment = new BitmapImage[] {
            new BitmapImage(new Uri(@"ms-appx:///Assets/baixogif.gif")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/cimagif.gif")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/dirgif.gif")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/esqgif.gif")),
        };

        BitmapImage[] parado = new BitmapImage[]
        {
             new BitmapImage(new Uri(@"ms-appx:///Assets/downAnimation/0.png")),
             new BitmapImage(new Uri(@"ms-appx:///Assets/upAnimation/0.png")),
             new BitmapImage(new Uri(@"ms-appx:///Assets/leftAnimation/0.png")),
             new BitmapImage(new Uri(@"ms-appx:///Assets/rightAnimation/0.png")),

        };

        private void StartAnimation() // Método para configuração e inicialização do timer da animação
        {
            if (!timer.IsEnabled)
            {
                timer.Tick += AnimationEvent;
                timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
                timer.Start();
            }

        }

        private void StopAnimation() //Método para parar a animação do movimento
        {
            timer.Stop();
        }


        private void AnimationEvent(object sender, object e) //Timer que roda o codigo escrito
        {                                                    // A cada 110 milisegundos       

            if (Up && PosY > 0)
            {

                if (!IsPlayerOverItem(Item, Up)) {
                MoveUp();
                PaintAnimation(upMoviment, 1);

                }

                else PosY += Velocity * 3;
            }
            if (Down && PosY < 570)
            {

                if(!IsPlayerOverItem(Item, Down))
                {
                MoveDown();
                PaintAnimation(upMoviment, 0);

                }
                else PosY -= Velocity * 2;
            }
            if (Left && PosX > 0)
            {
                if (!IsPlayerOverItem(Item, Left))
                {

                    MoveLeft();
                    PaintAnimation(upMoviment, 3);
                }
            else PosX += Velocity * 2;
            }
            if (Right && PosX < 770)
            {
                if (!IsPlayerOverItem(Item, Right))
                {
                    MoveRight();
                    PaintAnimation(upMoviment, 2);
                }
                else PosX -= Velocity * 2;
            }


        }

        public void PaintAnimation(BitmapImage[] MovimentArray, int i) // Método de recebe como parametro um vetor de Bitmap
        {                                                       // e realiza a animação do movimento
            Person1.Source = MovimentArray[i];
        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            PosY = Canvas.GetTop(Person1); //Armazena a posição Y do personagem em uma variavel
            PosX = Canvas.GetLeft(Person1); //Armazena a posição X do personagem em uma variavel

            Item.Source = parado[0];
            if (args.VirtualKey == Windows.System.VirtualKey.Up) 
            {
                StartAnimation();
                Up = true;
            }
            if (args.VirtualKey == Windows.System.VirtualKey.Down)
            {
                StartAnimation();
                Down = true;

            }
            if (args.VirtualKey == Windows.System.VirtualKey.Right)
            {
                StartAnimation();
                Right = true;
            }
            if (args.VirtualKey == Windows.System.VirtualKey.Left)
            {
                StartAnimation();
                Left = true;
            }
        }


        private void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            if (Up)
            {
                StopAnimation();
                Person1.Source = parado[1];
                MoveDown(3);
                Up = false;

            }
            if (Down)
            {
                StopAnimation();
                Person1.Source = parado[0];
                MoveUp(3);
                Down = false;
            }
            if (Left)
            {
                StopAnimation();
                Person1.Source = parado[2];
                MoveRight(3);
                Left = false;
            }
            if (Right)
            {
                StopAnimation();
                Person1.Source = parado[3];
                MoveLeft(3);

                Right = false;

            }
        }
        

        /// <summary>
        /// Checa se o player caminha sobre um item no mapa
        /// </summary>
        /// <param name="_item"></param>
        /// <returns></returns>
        public bool IsPlayerOverItem(Image _item, bool key)
        {

            if (PosX + Person1.Width >= Canvas.GetLeft(_item) &&
                PosX <= Canvas.GetLeft(_item) + _item.Width &&
                PosY + Person1.Height >= Canvas.GetTop(_item) &&
                PosY <= Canvas.GetTop(_item) + _item.Height
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
