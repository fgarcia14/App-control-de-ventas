using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Microcharts.Forms;
using SQLite;

namespace Examen
{
    public partial class Graficas : ContentPage
    {
       

        public void abrirBase(object sender, EventArgs e){

            //Crear la ruta donde se almacenara la base de datos
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "MiNegocio.db");
            
            //Abrir la base de datos y en caso de que no exista se creara
            var db = new SQLiteConnection(rutaDb);
            
            //Abrir la tabla de productos y en cso de que no exista se creara
            db.CreateTable<Modelos.Ventas>();
            db.CreateTable<Modelos.Productos>();
            
            
            //Cargar la lista de todos los productos a una arreglo
            string query = "SELECT COUNT(Ventas.IdVenta) as cantidad, Productos.NombreProducto as producto FROM Ventas JOIN Productos ON Ventas.Producto=Productos.IdProducto GROUP BY Productos.IdProducto ORDER BY 1 DESC LIMIT 5";

            List<Clases.GraficosProductos> p = null;
            p = db.Query<Clases.GraficosProductos>(query).ToList();

            //Cargar la lista de todos los clientes a una arreglo
            query = "SELECT COUNT(Ventas.IdVenta) as cantidad, Clientes.NombreCliente as cliente FROM Ventas JOIN Clientes ON Ventas.Cliente=Clientes.IdCliente GROUP BY Clientes.IdCliente ORDER BY 1 DESC LIMIT 5";
            List<Clases.GraficosClientes> c = null;
            c = db.Query<Clases.GraficosClientes>(query).ToList();

            mostrarGrafico(p,c);
        }

        List<Microcharts.Entry> _entriesProducto;
        List<Microcharts.Entry> _entriesCliente;

        private void mostrarGrafico(List<Clases.GraficosProductos> productos, List<Clases.GraficosClientes> clientes){
            _entriesProducto = null;
            _entriesProducto = new List<Microcharts.Entry>();
            foreach (var i in productos)
            {
                var pro = new Microcharts.Entry(i.cantidad)
                {
                    Label = i.producto,
                    ValueLabel = i.cantidad.ToString(),
                    Color=SKColor.Parse("#FF0033")
                };
                _entriesProducto.Add(pro);
            }

			_entriesCliente = null;
			_entriesCliente = new List<Microcharts.Entry>();

            foreach (var j in clientes)
            {
                var clie = new Microcharts.Entry(j.cantidad)
                {
                    Label = j.cliente,
                    ValueLabel = j.cantidad.ToString(),
                    Color = SKColor.Parse("#1A66FF")
                };
                _entriesCliente.Add(clie);
            }

            GraficoProducto.Chart = new BarChart { Entries = _entriesProducto };
            GraficoCliente.Chart = new BarChart { Entries = _entriesCliente };
        }


        public Graficas()
        {
            InitializeComponent();
            this.Appearing += abrirBase;
        }
    }
}
