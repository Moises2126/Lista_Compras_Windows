using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista_Compras_Windows
{
    public class Lista
    {
        public string Nome { get; set; }
        public List<Item> Items { get; private set; }

        public Lista()
        {
            Items = new List<Item>();


        }
    }

}
