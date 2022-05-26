using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista_Compras_Windows
{
    internal class Model
    {
        public List<Categoria> Categorias { get; set; }
        public List<Lista> Listas { get; private set; }

        public Model()
        {
            Categorias = new List<Categoria>();
            Listas = new List<Lista>(); 
        }
    }
}
