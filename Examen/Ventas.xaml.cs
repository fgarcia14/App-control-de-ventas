using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace Examen
{
    public partial class Ventas : ContentPage
    {
        public Ventas()
        {
            InitializeComponent();
            this.Appearing += show;

        }

        private void show(object sender, EventArgs e){
            abrirBase();
        }



        public void abrirBase(){
            //Crear la ruta donde se almacenara la base de datos
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "MiNegocio.db");

            //Abrir la base de datos y en caso de que no exista se creara
            var db = new SQLiteConnection(rutaDb);

            //Abrir la tabla de productos y en cso de que no exista se creara
            db.CreateTable<Modelos.Ventas>();
            db.CreateTable<Modelos.Clientes>();
            db.CreateTable<Modelos.Productos>();


            //Cargar la lista de todos los productos a una arreglo
            string query = "SELECT Ventas.IdVenta as IdVenta, Clientes.NombreCliente AS Cliente, Productos.NombreProducto as Producto, Ventas.Fecha AS Fecha, Ventas.Pagado AS Pagado FROM Clientes INNER JOIN Ventas ON Clientes.IdCliente = Ventas.Cliente INNER JOIN Productos ON Productos.IdProducto=Ventas.Producto";

            var todoslosproductos = db.Query<Clases.Vista>(query).ToList();

             //Limpiar lista
            lst.ItemsSource = null;
            lst.ItemsSource=todoslosproductos;


        }

        public async void agregar(object sender, EventArgs e){
            var nuevaVenta = new NuevaVenta();
            await Navigation.PushAsync(nuevaVenta);
        }
    }
}
