using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGlib.Characters; 


namespace RPGlib.Itens
{
    abstract public class Item //class generica de itens
    {
        public string Description; //descricao de cada item
        public string CaminhoImagem; //caminho (pasta) da imagem atribuida a cada item

        abstract public void Efeito(Character Person); //funcao que possibilita usar o item

        //passa personagem como parametro para possibilitar acesso aos seus atributos (?)
    }
}
