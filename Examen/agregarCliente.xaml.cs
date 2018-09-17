﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.IO;

namespace Examen
{
    public partial class agregarCliente : ContentPage
    {
    
        string directorio = "";
        string nombredearchivo = "";

        private void AgregarCliente()
        {
            //Definir la ruta donde estara el archivo de base de datos
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            string rutaDb = System.IO.Path.Combine(folder, "MiNegocio.db");

            //Abrir la base de datos
            var db = new SQLiteConnection(rutaDb);

            // Crea la tabla si no existe
            db.CreateTable<Modelos.Clientes>();

            var registro = new Modelos.Clientes
            {
                NombreCliente = nombre.Text,
                Telefono = telefono.Text,
                Correo = correo.Text,
                Comentarios = comentarios.Text,
                FotoCliente = foto.Text
            };
            //Insertar el registro
            db.Insert(registro);
            DisplayAlert("Agregar", "El registro fue agregado con exito!", "ok");

        }

        public agregarCliente()
        {
            InitializeComponent();
        }

        public async void tomarFoto(object sender, EventArgs e)
        {
            try
            {
                var Directorio = (Application.Current.Properties["directorio"].ToString());
                //await DisplayAlert("Directorio====", "!" + Directorio + "!", "ok");
                if (Directorio != null)
                {
                    directorio = Directorio;
                    //await DisplayAlert("Directorio====", "*" + directorio + "*", "ok");
                }
            }
            catch
            {
                //await DisplayAlert("Directorio====", "!valio madre!", "ok");
                directorio = "";
            }

            /*if (directorio != "")
            {
                string fileName1 = directorio + "/test.jpg";
                File.Delete(fileName1);
            }*/

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No hay Camara", ":( No hay camera disponible.", "OK");
                return;
            }

            var _mediaFile = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Fotos",
                Name = "test.jpg",
                SaveToAlbum = true

            });

            if (_mediaFile == null)
            {
                nombredearchivo = "";
                return;
            }
            if (directorio == "")
            {

                directorio = _mediaFile.Path;

                int inicio = directorio.IndexOf("Foto", 0);
                directorio = directorio.Substring(0, inicio + 5);
                //await DisplayAlert("Directorio", "<<<" + directorio + ">>>", "ok");
                Application.Current.Properties["directorio"] = directorio;
                await Application.Current.SavePropertiesAsync();

            }

            string mediaarchivo = _mediaFile.Path;
            int iniciop = mediaarchivo.IndexOf("Foto", 0);
            int final = mediaarchivo.Length;
            nombredearchivo = mediaarchivo.Substring(iniciop + 6, final - (iniciop + 6));

            //await DisplayAlert("Arhivo=", "*" + nombredearchivo + "*", "ok");
            //await DisplayAlert("File Location", _mediaFile.Path + " Publico:" + _mediaFile.AlbumPath, "OK");
            //ViewModel.StoreImageUrl(file.Path);

            await DisplayAlert("Instantanea", "La foto fue añadida a su coleccion.", "OK");

            foto.Text = directorio + "/" + nombredearchivo;

            PhotoImage.Source = mediaarchivo;
            /*
            PhotoImage.Source = ImageSource.FromStream(() =>
            {
                var stream = _mediaFile.GetStream();
                _mediaFile.Dispose();
                return stream;
            });
            */
        }

        public async void guardar(object sender, EventArgs e)
        {
            AgregarCliente();
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
