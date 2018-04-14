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
    public class RabbitFeet: Item
    {
        public RabbitFeet(int critical)
        {

            this.ImageItem = new BitmapImage(new Uri(@"ms-appx:///Assets/rabbit_feet.png")); //ADICIONAR IMAGEM DE UM PÉ DE COELHO
            this.ItemName = "Rabbit's Feet";
            this.Armor = 0;
            this.CriticalRate = critical;
            this.Damage = 0;
            this.EvasionRate = 0;
            this.Health = 0;
            this.Mana = 0;
            this.Lifesteal = 0;
            this.Description = "Critical Rate + " + critical+"%";
        }

        public override void Effect(Character person)
        {
            person.CriticRate += this.CriticalRate;
           
        }
    }
}
