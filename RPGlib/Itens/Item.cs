using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGlib.Characters;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace RPGlib.Itens
{
    abstract public class Item //class generica de itens
    {
        public string Description; //descricao de cada item
        public BitmapImage ImageItem { get; set; } //caminho (pasta) da imagem atribuida a cada item
        public string itemName { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int Health { get; set; }
        public int criticalRate { get; set; }
        public int evasionRate { get; set; }
        public int Mana { get; set; }
        //VELOCIDADE DO PERSONAGEM

        abstract public void Effect(Character Person); //funcao que possibilita usar o item

        //passa personagem como parametro para possibilitar acesso aos seus atributos (?)
    }
}