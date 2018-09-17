using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Examen
{
    public partial class Opciones : ContentPage
    {
        public Opciones()
        {
            InitializeComponent();
        }

        public async void clientes(object sender, EventArgs e){
            await Navigation.PushAsync(new verClientes());
        }

        public async void productos(object sender, EventArgs e){
            await Navigation.PushAsync(new verProductos());
        }
    }
}
