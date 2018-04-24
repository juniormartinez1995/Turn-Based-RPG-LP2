using RPGlib.Characters;
using RPGlib.Itens;
using RPGlib.Mobs;
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
    public sealed partial class Map2 : Page
    {
        public Map2()
        {
            this.InitializeComponent();
            ControllerGame.AdjustFullScreenMode(_Canvas, this);

            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }
        DispatcherTimer timer = new DispatcherTimer(); //Timer da animação
        List<Image> Collision = new List<Image>(); //Lista de colisões no mapa
        List<Image> LockedChests = new List<Image>(); //Lista de baús no mapa
        List<Image> Enemies = new List<Image>(); //Lista de enimigos no mapa
        List<Image> Keys = new List<Image>(); //Lista de chaves no mapa
        List<object> MobAndChar = new List<object>();

        Character Player; //Personagem que estará no mapa
        PablloVittar PablloVittar = new PablloVittar();
        Ninja Ninja = new Ninja();
        Chest ChestControl = new Chest(); //Gerenciamento do baú

        double WidthRatio, HeightRatio;
        double PosY, PosX; //Posição X e Y do personagem no mapa
        bool IsKeyPressed, Up, Down, Right, Left, IsAnotherPage; //Checagem da direção que o personagem está indo

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ControllerGame.CheckLastPage(typeof(SelecClass), this)) {
                Player = e.Parameter as Character;
                Person1.Source = Player.IdleRight;
                MobAndChar.Add(Player);

            }

            if (ControllerGame.CheckLastPage(typeof(BattleScreen), this)) {
                MobAndChar = e.Parameter as List<Object>;

                Player = MobAndChar.ElementAt(0) as Character;

                if (MobAndChar.ElementAt(1) is Ninja) Ninja = MobAndChar.ElementAt(1) as Ninja;
                else if (MobAndChar.ElementAt(1) is PablloVittar) PablloVittar = MobAndChar.ElementAt(1) as PablloVittar;
                MobAndChar.RemoveAt(MobAndChar.Count - 1);
                ControllerGame.PlaySoundsVitorHugo("CaioInimigosAFrente.mp4");

            }
            IsAnotherPage = false;
        }

        //Putaria pura daqui pra baixo -------

        private void ShowItemStatus1(object sender, TappedRoutedEventArgs e)
        {
            Item actual = Player.inventory.inventoryList[0];

            FlyoutBase.ShowAttachedFlyout((FrameworkElement) sender);
            ItemStatus1.Text = actual.ItemName + "\n" + actual.Description;
        }

        private void ShowItemStatus2(object sender, TappedRoutedEventArgs e)
        {
            Item actual = Player.inventory.inventoryList[1];

            FlyoutBase.ShowAttachedFlyout((FrameworkElement) sender);
            ItemStatus2.Text = actual.ItemName + "\n" + actual.Description;
        }

        private void ShowItemStatus3(object sender, TappedRoutedEventArgs e)
        {
            Item actual = Player.inventory.inventoryList[2];

            FlyoutBase.ShowAttachedFlyout((FrameworkElement) sender);
            ItemStatus3.Text = actual.ItemName + "\n" + actual.Description;
        }

        private void ShowItemStatus4(object sender, TappedRoutedEventArgs e)
        {
            Item actual = Player.inventory.inventoryList[3];

            FlyoutBase.ShowAttachedFlyout((FrameworkElement) sender);
            ItemStatus4.Text = actual.ItemName + "\n" + actual.Description;
        }

        private void ShowItemStatus5(object sender, TappedRoutedEventArgs e)
        {
            Item actual = Player.inventory.inventoryList[4];

            FlyoutBase.ShowAttachedFlyout((FrameworkElement) sender);
            ItemStatus5.Text = actual.ItemName + "\n" + actual.Description;
        }

        private void ShowItemStatus6(object sender, TappedRoutedEventArgs e)
        {
            Item actual = Player.inventory.inventoryList[5];

            FlyoutBase.ShowAttachedFlyout((FrameworkElement) sender);
            ItemStatus6.Text = actual.ItemName + "\n" + actual.Description;
        }
    
    }

}
