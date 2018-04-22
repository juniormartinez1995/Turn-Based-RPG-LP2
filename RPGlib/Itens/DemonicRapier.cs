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
    public class DemonicRapier : Item
    {
        public DemonicRapier(int damage)
        {

            this.ImageItem = new BitmapImage(new Uri(@"ms-appx:///Assets/DemonicRapier.png"));  //ADICIONAR IMAGEM DE ESPADA
            this.ItemName = "Demonic Rapier";
            this.Armor = 0;
            this.CriticalRate = 0;
            this.Damage = damage;
            this.EvasionRate = 0;
            this.Health = 0;
            this.Mana = 0;
            this.Lifesteal = 0;
            this.Description = "Damage + " + damage;
            this.Speed = 0;
        }

        public override void Effect(Character person)
        {
            person.Damage += this.Damage;
       
        }
    }
}
