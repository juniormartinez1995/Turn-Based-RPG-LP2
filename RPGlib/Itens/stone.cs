using RPGlib.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace RPGlib.Itens
{
    public class Stone : Item
    {

        public Stone(int _Speed)
        {
            this.Health = 0;
            this.ItemName = "Pedra";
            this.Description = "Tem uma pedra no seu caminho.";
            this.ImageItem = new BitmapImage(new Uri(@"ms-appx:///Assets/stone.png")); 

            //---------------------------------------------

            this.Armor = 0;
            this.CriticalRate = 0;
            this.EvasionRate = 0;
            this.Damage = 0;
            this.Mana = 0;
            this.Lifesteal = 0;
            this.Speed = _Speed;
            this.Description = "Speed - " + _Speed + "%";
        }

        public override void Effect(Character person)
        {
            person.Speed -= this.Speed;

        }

    }
}
