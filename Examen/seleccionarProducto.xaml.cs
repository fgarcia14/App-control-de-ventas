using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace Examen
{
    public partial class seleccionarProducto :  ContentPage
    {

        public void AbrirBase()
        {
            //Crear la ruta donde se almacenara la base de datos
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "MiNegocio.db");

            //Abrir la base de datos y en caso de que no exista se creara
            var db = new SQLiteConnection(rutaDb);

            //Abrir la tabla de productos y en cso de que no exista se creara
            db.CreateTable<Modelos.Productos>();

            //Cargar la lista de todos los productos a una arreglo
            var todoslosproductos = db.Table<Modelos.Productos>().ToList();

            //mostrarla en el lisview perzonalizado para ver los datos de los productos
            lst.ItemsSource = null;
            lst.ItemsSource = todoslosproductos;
        }

        private void seleccionarProducto_Appearing(object sender, EventArgs e)
        {
            AbrirBase();
        }

        NuevaVenta v;

        public seleccionarProducto(NuevaVenta v)
        {
            InitializeComponent();
            this.v = v;
            this.Appearing += seleccionarProducto_Appearing;
        }


        public async void agregar(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new agregarProductos());
            AbrirBase();
        }

        private async void ItemSeleccionado(object sender, ItemTappedEventArgs e)
        {
            var producto = e.Item as Modelos.Productos;
            v.BindingContext = producto;
            await Navigation.PopAsync();
        }
    }
}
