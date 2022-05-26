using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista_Compras_Windows
{
    internal class Item
    {
        public string Nome { get; set; }
        public string Quantidade { get; set; }
        public string Categoria { get; set; }
        public bool Comprado { get; set; }
    }
}
