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
    public class DraculaTeeth : Item
    {
        public DraculaTeeth(int lifesteal)
        {

            this.ImageItem = new BitmapImage(new Uri(@"ms-appx:///Assets/draculasteeth.png"));  
            this.ItemName = "Dracula's Teeth";
            this.Armor = 0;
            this.CriticalRate = 0;
            this.Damage = 0;
            this.EvasionRate = 0;
            this.Health = 0;
            this.Mana = 0;
            this.Lifesteal = lifesteal;
            this.Description = "Lifesteal + " + lifesteal+"%";
            this.Speed = 0;
        }

        public override void Effect(Character person)
        {
            person.Lifesteal += this.Lifesteal;

        }
    }
}
