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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace RPG_LP2
{
    public static class ControllerGame
    {

        public static String SizeHit;
        //Método para checar a página anterior
        public static bool CheckLastPage(Type desiredPage, Page CurrentPage)
        {
            var lastPage = CurrentPage.Frame.BackStack.LastOrDefault();
            return (lastPage != null && lastPage.SourcePageType.Equals(desiredPage)) ? true : false;
        }

        public static void AdjustFullScreenMode(Canvas _Canvas, Page CurrentPage)
        {
            double WidthRatio, HeightRatio;

            List<UIElement> PageElements = _Canvas.Children.ToList();



            var bounds = Window.Current.Bounds;
            double height = bounds.Height;
            Debug.WriteLine(height);
            double width = bounds.Width;
            Debug.WriteLine(width);

            CurrentPage.Width = bounds.Width;
            CurrentPage.Height = bounds.Height;
            _Canvas.Width = CurrentPage.Width;
            _Canvas.Height = CurrentPage.Height;


            WidthRatio = _Canvas.Width / 800;
            HeightRatio = _Canvas.Height / 600;

            foreach (UIElement Element in PageElements)
            {
                Canvas.SetLeft(Element, Canvas.GetLeft(Element) * WidthRatio);
                Canvas.SetTop(Element, Canvas.GetTop(Element) * HeightRatio);

                (Element as FrameworkElement).Width *= WidthRatio;
                (Element as FrameworkElement).Height *= HeightRatio;
            }

        }

        public static void PaintAnimation(Image Person1, Character Person, bool Right, bool Left, bool Up, bool Down) // Método de recebe como parametro um vetor de Bitmap
        {                                                       // e realiza a animação do movimento
            if (Right) Person1.Source = Person.RightMoviment;
            if (Left) Person1.Source = Person.LeftMoviment;
            if (Up) Person1.Source = Person.UpMoviment;
            if (Down) Person1.Source = Person.DownMoviment;
        }

        public static bool CheckEnemy(Character PlayerObject, Image Player, List<Image> Enemies, int i)
        {
            if (CheckCollision(PlayerObject, Player, Enemies.ElementAt(i))) return true;
            return false;
        }



        public static bool CheckListCollision(Character PlayerObject, Image Player, List<Image> ItemList)
        {
            foreach (Image Item in ItemList)
            {
                if (CheckCollision(PlayerObject, Player, Item)) return true;
            }
            return false;
        }

        public static bool IsMovimentAllowed(Character PlayerObject, Image Player, List<Image> LockedChests, List<Image> Enemies, List<Image> Collision, List<Image> Keys)
        {
            return !CheckListCollision(PlayerObject, Player, LockedChests) && !CheckListCollision(PlayerObject, Player, Enemies) && !CheckListCollision(PlayerObject, Player, Collision) && !CheckListCollision(PlayerObject, Player, Keys);
        }

        public static bool IsSkillHittingEnemy(Image Player, Image Enemy)
        {

            if (Canvas.GetLeft(Player) + Player.Width >= Canvas.GetLeft(Enemy) &&
                Canvas.GetLeft(Player) <= Canvas.GetLeft(Enemy) + Enemy.Width &&
                Canvas.GetTop(Player) + Player.Height >= Canvas.GetTop(Enemy) &&
                Canvas.GetTop(Player) <= Canvas.GetTop(Enemy) + Enemy.Height
                )
            {
                return true;

            }
            else return false;
        }


        public static bool IsSkillHittingPerson(Image Player, Image Enemy)
        {

            if (Canvas.GetLeft(Player) + Player.Width >= Canvas.GetLeft(Enemy) &&
                Canvas.GetLeft(Player) <= Canvas.GetLeft(Enemy) + Enemy.Width &&
                Canvas.GetTop(Player) + Player.Height >= Canvas.GetTop(Enemy) &&
                Canvas.GetTop(Player) <= Canvas.GetTop(Enemy) + Enemy.Height
                ) {
                return true;

            }
            else return false;
        }
        public static bool CheckCollision(Character PlayerObject, Image Player, Image ObjectCollided)
        {
            double PlayerRight = Canvas.GetLeft(Player) + Player.Width;
            double PlayerLeft = Canvas.GetLeft(Player);
            double PlayerTop = Canvas.GetTop(Player);
            double PlayerBottom = Canvas.GetTop(Player) + Player.Height;

            double EnemyRight = Canvas.GetLeft(ObjectCollided) + ObjectCollided.Width;
            double EnemyLeft = Canvas.GetLeft(ObjectCollided);
            double EnemyTop = Canvas.GetTop(ObjectCollided);
            double EnemyBottom = Canvas.GetTop(ObjectCollided) + ObjectCollided.Height;

            if (PlayerRight >= EnemyLeft &&
                PlayerLeft <= EnemyRight &&
                PlayerBottom >= EnemyTop &&
                PlayerTop <= EnemyBottom
                )
            {
                if (Collision(Player, ObjectCollided) == "Right") MovePlayer(Player, PlayerObject.Speed - 2, 0);   //MoveRight(Player, 2);
                if (Collision(Player, ObjectCollided) == "Left") MovePlayer(Player, -PlayerObject.Speed + 2, 0);         //MoveLeft(Player, 2);
                if (Collision(Player, ObjectCollided) == "Top") MovePlayer(Player, 0, -PlayerObject.Speed + 2); //MoveUp(Player, 2);
                if (Collision(Player, ObjectCollided) == "Bottom") MovePlayer(Player, 0, PlayerObject.Speed - 2);
                return true;

            }
            else return false;
        }

        public static String Collision(Image Player, Image ObjectCollided)
        {

            double player_bottom = Canvas.GetTop(Player) + Player.Height;
            double enemy_bottom = Canvas.GetTop(ObjectCollided) + ObjectCollided.Height;
            double player_right = Canvas.GetLeft(Player) + Player.Width;
            double enemy_right = Canvas.GetLeft(ObjectCollided) + ObjectCollided.Width;

            double b_collision = enemy_bottom - Canvas.GetTop(Player);
            double t_collision = player_bottom - Canvas.GetTop(ObjectCollided);
            double l_collision = player_right - Canvas.GetLeft(ObjectCollided);
            double r_collision = enemy_right - Canvas.GetLeft(Player);

            if (t_collision < b_collision && t_collision < l_collision && t_collision < r_collision)
            {
                //Top collision
                SizeHit = "Top"; ;
                Debug.WriteLine(SizeHit);

            }
            if (b_collision < t_collision && b_collision < l_collision && b_collision < r_collision)
            {
                //bottom collision
                SizeHit = "Bottom";
                Debug.WriteLine(SizeHit);
            }
            if (l_collision < r_collision && l_collision < t_collision && l_collision < b_collision)
            {
                //Left collision
                SizeHit = "Left"; ;
                Debug.WriteLine(SizeHit);
            }
            if (r_collision < l_collision && r_collision < t_collision && r_collision < b_collision)
            {

                //Right collision
                SizeHit = "Right";
                Debug.WriteLine(SizeHit);
            }

            return SizeHit;
        }

        public static void LootVault(Character player, Chest ChestControl, TextBlock qt_lifePot, TextBlock qt_manaPot, List<InventoryBitImage> ListImage)
        {
            if (!ChestControl.isOpen) //Abre o baú e adiciona os itens ao inventário
            {
                PlaySoundsRPG("SoundOpenChest.mp3");
                player.OpenChest(ChestControl);
                qt_lifePot.Text = player.inventory.inventoryPotionLife.Count().ToString();
                qt_manaPot.Text = player.inventory.inventoryPotionMana.Count().ToString();

                int countItem = 0;
                PlaySoundsVitorHugo("CaioItem.mp4");
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

        public static void MovePlayer(Image Player, double XSpeed, double YSpeed)
        {
            Canvas.SetLeft(Player, Canvas.GetLeft(Player) + XSpeed);
            Canvas.SetTop(Player, Canvas.GetTop(Player) + YSpeed);
        }

        public static async void PlaySoundsRPG(string nomeMusic)
        {
            MediaElement Music = new MediaElement();

            StorageFolder Folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            Folder = await Folder.GetFolderAsync("Assets");
            StorageFile sf = await Folder.GetFileAsync(nomeMusic);
            Music.Volume = 0.5;
            Music.SetSource(await sf.OpenAsync(FileAccessMode.Read), sf.ContentType);
            Music.IsLooping = true;
            Music.Play();
        }
        public static async void PlayAmbienceMap(string nomeMusic)
        {
            MediaElement Music = new MediaElement();

            StorageFolder Folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            Folder = await Folder.GetFolderAsync("Assets");
            StorageFile sf = await Folder.GetFileAsync(nomeMusic);
            Music.Volume = 0.5;
            Music.SetSource(await sf.OpenAsync(FileAccessMode.Read), sf.ContentType);
            Music.Play();
            Music.IsLooping = true;

        }
        public static async void PlaySoundsVitorHugo(string nomeMusic)
        {
            MediaElement Music = new MediaElement();

            StorageFolder Folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            Folder = await Folder.GetFolderAsync("Assets");
            StorageFile sf = await Folder.GetFileAsync(nomeMusic);
            Music.Volume = 0.1;
            Music.SetSource(await sf.OpenAsync(FileAccessMode.Read), sf.ContentType);
            Music.Play();
            Music.IsLooping = true;

        }


    }
}
