using RPGlib.Characters;
using RPGlib.Itens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace RPG_LP2
{
    public static class ControllerGame
    {

        public static void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey) //Detecta qual direção o personagem irá ir
            {
                case Windows.System.VirtualKey.Up:
                    Debug.WriteLine("Controller Up");
                    break;
                case Windows.System.VirtualKey.Down:
                    Debug.WriteLine("Controller Down");
                    break;
                case Windows.System.VirtualKey.Left:
                    Debug.WriteLine("Controller Left");
                    break;
                case Windows.System.VirtualKey.Right:
                    Debug.WriteLine("Controller Right");
                    break;
            }


        }

        //CAIO CHUPETA

        public static void PaintAnimation(Image Person1, Character Person, bool Right, bool Left, bool Up, bool Down) // Método de recebe como parametro um vetor de Bitmap
        {                                                       // e realiza a animação do movimento
            if (Right) Person1.Source = Person.RightMoviment;
            if (Left) Person1.Source = Person.LeftMoviment;
            if (Up) Person1.Source = Person.UpMoviment;
            if (Down) Person1.Source = Person.DownMoviment;
        }

        //Checa se o movimento é permitido, se nao enconsta em um bau, inimigo ou obstaculo
        public static bool IsMovimentAllowed(Image Person1, List<Image> LockedChests, List<Image> Enemies, List<Image> Collision, bool key)
        {
            return !IsPlayerOverChest(Person1, LockedChests, key) && !IsPlayerColliding(Person1, Enemies, key) && !IsPlayerColliding(Person1, Collision, key);
        }


        public static bool IsPlayerOverChest(Image Person1, List<Image> LockedChests, bool key) //Checa se o personagem encontrou um baú no mapa
        {


            foreach (Image vault in LockedChests)
            {
                if (IsPlayerOverItem(Person1, vault, key)) return true;
            }

            return false;
        }

        public static bool IsPlayerColliding(Image Person1, List<Image> Collision, bool key) //Checa se o personagem colide com algum objeto e/ou personagem
        {

            foreach (Image wall in Collision)
            {
                if (IsPlayerOverItem(Person1, wall, key)) return true;
            }
            return false;

        }


        public static bool IsPlayerOverItem(Image Person1, Image _item, bool key)
        {

            if (Canvas.GetLeft(Person1) + Person1.Width >= Canvas.GetLeft(_item) &&
                Canvas.GetLeft(Person1) <= Canvas.GetLeft(_item) + _item.Width &&
                Canvas.GetTop(Person1) + Person1.Height >= Canvas.GetTop(_item) &&
                Canvas.GetTop(Person1) <= Canvas.GetTop(_item) + _item.Height
                )
            {
                return key;

            }
            else return false;
        }

        public static bool CheckEnemy(Image Person1, List<Image> Enemy, bool key, int i)
        {
            if (IsPlayerOverItem(Person1, Enemy.ElementAt(i), key)) return true;
            return false;
        }


        public static void LootVault(Character player, Chest ChestControl, TextBlock qt_lifePot, TextBlock qt_manaPot, List<InventoryBitImage>ListImage)
        {   
            if (!ChestControl.isOpen) //Abre o baú e adiciona os itens ao inventário
            {
                player.OpenChest(ChestControl);
                qt_lifePot.Text = player.inventory.inventoryPotionLife.Count().ToString();
                qt_manaPot.Text = player.inventory.inventoryPotionMana.Count().ToString(); //CAIO SEU BURRO FDP CHUPETINHA

                int countItem = 0;
                foreach (Item item in player.inventory.inventoryList)
                {
                    ListImage[countItem].BitImage = item.ImageItem;
                    countItem++;
                }
                foreach (InventoryBitImage BitmapImage in ListImage) 
                {
                    BitmapImage.ImageMap.Source = BitmapImage.BitImage;
                }

            }
        }

        public static void MoveUp(Image Person1, int Velocity, int Increment = 0) //Método que realiza a movimentação da imagem para cima
        {
            Person1.SetValue(Canvas.TopProperty, Canvas.GetTop(Person1) - Velocity - Increment);
        }

        public static void MoveDown(Image Person1, int Velocity, int Increment = 0) //Método que realiza a movimentação da imagem para baixo
        {
            Person1.SetValue(Canvas.TopProperty, Canvas.GetTop(Person1) + Velocity + Increment);
        }

        public static void MoveLeft(Image Person1, int Velocity, int Increment = 0) //Método que realiza a movimentação da imagem para esquerda
        {
            Person1.SetValue(Canvas.LeftProperty, Canvas.GetLeft(Person1) - Velocity - Increment);
        }

        public static void MoveRight(Image Person1, int Velocity, int Increment = 0) //Método que realiza a movimentação da imagem para direita
        {
            Person1.SetValue(Canvas.LeftProperty, Canvas.GetLeft(Person1) + Velocity + Increment);
        }

        public static async void PlayMusic(string nomeMusic)
        {
            MediaElement Music = new MediaElement();

            StorageFolder Folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            Folder = await Folder.GetFolderAsync("Assets");
            StorageFile sf = await Folder.GetFileAsync(nomeMusic);
            Music.SetSource(await sf.OpenAsync(FileAccessMode.Read), sf.ContentType);
            Music.IsLooping = true;
            Music.Play();
        }
    }
}
