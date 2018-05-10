using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace RPG_LP2.Controller
{
    static class SoundController
    {
        public static void PlaySound(MediaElement Sound, String SoundName, double volume = 0.3)
        {
            Sound.Source = new Uri(@"ms-appx:///Assets/" + SoundName +".mp4");
            Sound.Volume = volume;
            Sound.Play();
        }
    }
}
