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
using Windows.Media;
using Windows.Media.Playback;
using Windows.Media.Core;
using Windows.Storage;
using System.Diagnostics;


// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x416

namespace RPG_LP2
{

    public sealed partial class MainPage : Page
    {


        public MainPage()
        {

            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
            this.InitializeComponent();
            ControllerGame.AdjustFullScreenMode(_Canvas, this);

            //ControllerGame.PlayMusic("1 Hora de Musicas de Cidades e Vilarejos de RPG.mp3");
        }

        //funcao buttom de quit do jogo
        private void Btn_Close_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Application.Current.Exit();//fecha o programa
        }
        //funcao buttom de start do jogo
        private void Btn_Start_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SceneHistory));//proxima tela
        }
    }
}



