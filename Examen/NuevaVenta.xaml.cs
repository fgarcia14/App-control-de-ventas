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
    public partial class NuevaVenta : ContentPage
    {
        public NuevaVenta()
        {
            InitializeComponent();
            this.Appearing += actualizar;
        }

        Modelos.Clientes c;
        Modelos.Productos p;

        private void actualizar(object sender, EventArgs e){
            if (BindingContext is Modelos.Clientes)
            {
                c = (Modelos.Clientes)BindingContext;
                actualizarCliente();
            }
            else if (BindingContext is Modelos.Productos)
            {
                p = (Modelos.Productos)BindingContext;
                actualizarProducto();
            }
        }

        private void actualizarCliente(){
            usuario.Text = c.NombreCliente;
        }

        private void actualizarProducto(){
            producto.Text = p.NombreProducto;
        }

       
        public async void seleccionarCliente(object sender, EventArgs e){
            await Navigation.PushAsync(new seleccionarCliente(this));
        }

        public async void seleccionarProducto(object sender, EventArgs e){
            await Navigation.PushAsync(new seleccionarProducto(this)); 
        }

        public async void guardar(object sender, EventArgs e){
            if (usuario.Text=="" || producto.Text=="")
            {
                await DisplayAlert("Advertencia", "Seleccione un usuario y un producto", "Aceptar");
            }else{
                agregarVenta();
                await DisplayAlert("Ventas", "Venta agregada con exito", "Aceptar");
                await Navigation.PopAsync();
            }
        }

        private void agregarVenta()
        {
            //Definir la ruta donde estara el archivo de base de datos
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "MiNegocio.db");

            //Abrir la base de datos
            var db = new SQLiteConnection(rutaDb);

            // Crea la tabla si no existe
            db.CreateTable<Modelos.Ventas>();
            string tPagado;
            if (pagado.IsToggled)
            {
                tPagado = "Si";
            }
            else
            {
                tPagado = "No";
            }
            var registro = new Modelos.Ventas
            {
                Cliente = c.IdCliente,
                Producto = p.IdProducto,
                Fecha = fecha.Date,
                Pagado = tPagado
            };
            //Insertar el registro
            db.Insert(registro);

        }
    }
}
