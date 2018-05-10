using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace RPG_LP2.Controller
{
    static class SoundController
    {
        public static void PlaySound(MediaElement Sound, String SoundName, double volume = 0.3)
        {
            Sound.Source = new Uri(@"ms-appx:///Assets/" + SoundName);
            Sound.Volume = volume;
            Sound.Play();
        }


        public static async void PlayDynamicSound(string nomeMusic)
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
