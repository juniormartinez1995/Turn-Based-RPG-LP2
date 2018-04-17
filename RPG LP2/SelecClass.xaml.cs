﻿using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using RPGlib.Characters;


// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace RPG_LP2
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class SelecClass : Page
    {
        public SelecClass()
        {
            this.InitializeComponent();
            ControllerGame.AdjustFullScreenMode(_Canvas, this);
        }
        //ao user clickar nesse buttom, cria o personagem berserker 
        private void B_Berserker_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Berserker p1 = new Berserker();//cria o obj berserker
            this.Frame.Navigate(typeof(Map), p1); //vai para proxima tela
        }

        private void B_Dicer_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Dicer p1 = new Dicer(); //cria o obj dicer
           this.Frame.Navigate(typeof(Map), p1); //vai para proxima tela

        }
    }
}
