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
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace RPG_LP2
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class SceneHistory : Page
    {
        int btn = 0;
        bool SkipPageAllowed = false;

        public SceneHistory()
        {
            this.InitializeComponent();
        }

        private void btn_go_event(object sender, TappedRoutedEventArgs e)
        {
            //Mudar pra apresentaçao amanha. nao mexe nisso plz
            //if (SkipPageAllowed)
            //{
            //}
                if (btn == 0) { text_block_scene_history.Text = "Então seu nome é Caio?"; btn++; }
                else if (btn == 1) { text_block_scene_history.Text = "Caio Mendes?"; btn++; }
                else this.Frame.Navigate(typeof(SelecClass));
        }

        private void History_MediaEnded(object sender, RoutedEventArgs e)
        {
            //SkipPageAllowed = true;
        }
    }
}
