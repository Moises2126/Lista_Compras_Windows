using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml.Linq;

namespace LPDSW_DemoXml
{
    public partial class MainWindow : Window
    {
        Model _myModel = new Model();

        public MainWindow()
        {
            InitializeComponent();

            CarregaRepositorio();  // carrega para o Model os dados existentes em ficheiro 

            VisualizaRepositorio(); // força a visualização dos dados existentes no Model
        }

        private void CarregaRepositorio()
        {
            XDocument doc = XDocument.Load("MyListasXMLFile.xml");

            var cats = from al in doc.Descendants("Categoria") select al;

            foreach (var aux in cats)
            {
                Categoria c = new Categoria();
                c.Nome = aux.Attribute("nome").Value;
                c.fixa = aux.Attribute("fixa").Value == "true";

                _myModel.Categorias.Add(c);
            }

            var listas = from lst in doc.Root.Elements("Listas").Descendants("Lista") select lst;

            foreach (var aux in listas)
            {
                Lista nova = new Lista() { Nome = aux.Attribute("nome").Value };

                var produtos = from al in aux.Descendants("Produto") select al;

                foreach (var tmp in produtos)
                {
                    Item p = new Item();
                    p.Nome = tmp.Attribute("nome").Value;
                    p.Quantidade = tmp.Attribute("quantidade").Value;
                    p.Categoria = tmp.Attribute("categoria").Value;
                    p.Comprado = tmp.Attribute("comprado").Value == "true";

                    nova.Produtos.Add(p);
                }
                _myModel.Listas.Add(nova);
            }
        }

        private void VisualizaRepositorio()
        {
            lbCategorias.ItemsSource = _myModel.Categorias;

            cbListas.ItemsSource = _myModel.Listas;
        }

        private void cbListas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbListas.SelectedIndex >= 0)
            {
                lvProdutos.ItemsSource = _myModel.Listas[cbListas.SelectedIndex].Produtos;
            }

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvProdutos.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Categoria");
            view.GroupDescriptions.Clear();
            view.GroupDescriptions.Add(groupDescription);
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            XDocument doc = new XDocument();

            // não é necessário adicionar a declaração XML

            doc.Add(new XElement("ListasCompras")); // adiciona ao documento o nó raiz
            doc.Element("ListasCompras").Add(new XElement("Categorias")); // adiciona o nó para guardar as Categorias
            doc.Element("ListasCompras").Add(new XElement("Listas")); // adiciona o nó para guardar as listas de compras

            XElement cats = doc.Root.Element("Categorias");

            foreach (Categoria aux in _myModel.Categorias)
            {
                XElement no = new XElement("Categoria");
                no.Add(new XAttribute("nome", aux.Nome));
                no.Add(new XAttribute("fixa", aux.fixa));

                cats.Add(no);
            }

            XElement lists = doc.Root.Element("Listas");

            foreach (Lista aux in _myModel.Listas)
            {
                XElement no = new XElement("Lista");

                foreach (Produto p in aux.Produtos)
                {
                    XElement tmpProd = new XElement("Produto");
                    tmpProd.Add(new XAttribute("nome", p.Nome));
                    tmpProd.Add(new XAttribute("quantidade", p.Quantidade));
                    tmpProd.Add(new XAttribute("categoria", p.Categoria));
                    tmpProd.Add(new XAttribute("comprado", p.Comprado));

                    no.Add(tmpProd);
                }
                lists.Add(no);
            }
            doc.Save("MyListasXMLFileOutput.xml"); //nome modificado para não alterar o original

            MessageBox.Show("Dados guardados no ficheiro\n\"ListasXMLFileOutput.xml\"\n(procurar na pasta de execução da aplicação)");
        }
    }
}
