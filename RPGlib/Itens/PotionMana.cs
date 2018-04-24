using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGlib.Characters;
using Windows.UI.Xaml.Media.Imaging;

namespace RPGlib.Itens
{
    public class PotionMana : Item
    {

        //Description = descricao da acao do item
        //LocalImage = caminho da imagem q representa o item

        public PotionMana()
        {
            this.Mana = 50;
            this.ItemName = "Poção de Mana";
            this.Description = "bufa " + this.Mana + ".";
            this.ImageItem = new BitmapImage(new Uri(@"ms-appx:///Assets/potion_mana.png")); ;
            this.Health = 0;
            this.Armor = 0;
            this.CriticalRate = 0;
            this.EvasionRate = 0;
            this.Damage = 0;
            this.Lifesteal = 0;
            this.Speed = 0;
        }
        //person = personagem
        //correnMana = mana atual do personagem
        //maxMana = mana maxima do personagem
        public override void Effect(Character person)
        {
            if (person.CurrentMana + this.Mana >= person.MaxMana) //verifica se a soma da mana atual do personagem
            {// mais a mana fornecida for maior ou igual a mana maxima do personagem, se sim:

                person.CurrentMana = person.MaxMana;// a mana atual va ser igual a mana maxima
            }
            else//se nao:
            {
                person.CurrentMana = person.CurrentMana + this.Mana; // a mana atual do personagem vai ser igual a mana
                //atual mais a mana fornecida
            }
        }
    }
}