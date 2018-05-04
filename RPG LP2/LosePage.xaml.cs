using RPGlib.Characters;
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
    public sealed partial class LosePage : Page
    {
        Character Player;

        public LosePage()
        {
            this.InitializeComponent();      

            ControllerGame.AdjustFullScreenMode(_Canvas, this);
        }

        private void Close_rpg(object sender, TappedRoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void Restart_rpg(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ControllerGame.CheckLastPage(typeof(BattleScreen), this)) {

                Player = e.Parameter as Character;
                img_dy.Source = Player.Dying;
            }

        }

    }
}
