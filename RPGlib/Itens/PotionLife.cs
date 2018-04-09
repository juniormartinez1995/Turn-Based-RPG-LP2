using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGlib.Characters;
using RPGlib.Itens;
using Windows.UI.Xaml.Media.Imaging;

namespace RPGlib.Itens
{
    public class PotionLife : Item
    {
        //Description = descricao da acao do item
        //LocalImage = caminho da imagem q representa o item
        public PotionLife()
        {
            this.Health = 50;
            this.ItemName = "Poção de Vida";
            this.Description = "bufa " + this.Health + ".";
            this.ImageItem = new BitmapImage(new Uri(@"ms-appx:///Assets/potion_life.png")); ;

            //---------------------------------------------

            this.Armor = 0;
            this.CriticalRate = 0;
            this.EvasionRate = 0;
            this.Damage = 0;
            this.Mana = 0;

        }
        //person = personagem
        //currentHP = vida atual do personagem
        //maxHearth = vida maxima do personagem

        public override void Effect(Character person)
        {
            if (person.CurrentHP + Health > person.MaxHealth)//verifica se a soma da vida atual do personagem
            {// mais a vida fornecida for maior ou igual a mana maxima do personagem, se sim:

                person.CurrentHP = person.MaxHealth; // a vida atual vai ser igual a vida maxima
            }
            else //se nao:
            {
                person.CurrentHP = person.CurrentHP + Health;// a vida atual do personagem vai ser igual a vida
                //atual mais a vida fornecida
            }
        }
    }
}