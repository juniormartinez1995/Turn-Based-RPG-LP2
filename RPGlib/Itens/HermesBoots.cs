using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGlib.Itens;
using RPGlib.Characters;
using Windows.UI.Xaml.Media.Imaging;

namespace RPGlib.Itens
{
    public class HermesBoots : Item
    {
        public HermesBoots(int _Speed) //A MUDAR ATRIBUTOS PARA SPEED
        {

            this.ImageItem = new BitmapImage(new Uri(@"ms-appx:///Assets/hermesboots.png"));
            this.ItemName = "Hermes's Boots";
            this.Armor = 0;
            this.CriticalRate = 0;
            this.Damage = 0;
            this.EvasionRate = 0;
            this.Health = 0;
            this.Mana = 0;
            this.Lifesteal = 0;
            this.Speed = _Speed;
            this.Description = "Speed + " + _Speed + "%";
        }

        public override void Effect(Character person)
        {
            person.Speed += this.Speed;

        }
    }
}
