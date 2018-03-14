using RPGlib.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Windows.UI.Xaml.Controls;
using Windows.Storage;

namespace RPGlib
{
    
    public static class ControllerGame
    {
        

        public static void createBerserker()
        {
            Berserker p1 = new Berserker();
        }
        
        private void Play()
        {
            MediaElement PlayMusic = new MediaElement();

            StorageFolder Folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            Folder = await Folder.GetFolderAsync("Assets");
            StorageFile sf = await Folder.GetFileAsync("1 Hora de Musicas de Cidades e Vilarejos de RPG.mp3");
            PlayMusic.SetSource(await sf.OpenAsync(FileAccessMode.Read), sf.ContentType);
            PlayMusic.IsLooping = true;
            PlayMusic.Play();


        }
        Thread play = new Thread(Play());
        
    }
}

