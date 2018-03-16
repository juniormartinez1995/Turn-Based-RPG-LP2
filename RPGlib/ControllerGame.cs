using RPGlib.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using RPGlib.Itens;
using Windows.UI.Xaml.Media.Imaging;

namespace RPGlib
{

    public static class ControllerGame
    {


        public static void createBerserker()
        {
            Berserker p1 = new Berserker();
        }


        public static void PaintAnimation(Image Person1, Character Person, bool Right, bool Left, bool Up, bool Down) // Método de recebe como parametro um vetor de Bitmap
        {                                                       // e realiza a animação do movimento
            if (Right) Person1.Source = Person.RightMoviment;
            if (Left) Person1.Source = Person.LeftMoviment;
            if (Up) Person1.Source = Person.UpMoviment;
            if (Down) Person1.Source = Person.DownMoviment;
        }


        public static bool IsPlayerOverChest(Image Person1, List<Image> LockedChests , bool key) //Checa se o personagem encontrou um baú no mapa
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


        public static void LootVault(Character player, Chest ChestControl, TextBlock qt_lifePot, TextBlock qt_manaPot, List<BitmapImage> InventoryImage, List<Image> InventoryMap)
        {
            if (!ChestControl.isOpen) //Abre o baú e adiciona os itens ao inventário
            {
                player.OpenChest(ChestControl);
                qt_lifePot.Text = player.inventory.inventoryPotionLife.Count().ToString();
                qt_manaPot.Text = player.inventory.inventoryPotionMana.Count().ToString(); //LEO SEU BURRO FDP CHUPETINHA

                foreach (Item item in player.inventory.inventoryList)
                {
                    InventoryImage.Add(item.ImageItem);
                }

                for (int y = 0; y < InventoryImage.Count(); y++) //Coloca os Bitmaps das Imagens na lista de Imagens visuais
                {
                    InventoryMap[y].Source = InventoryImage[y];
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

    }
}

