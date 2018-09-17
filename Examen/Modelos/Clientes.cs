using System;
using SQLite;
namespace Examen.Modelos
{
    public class Clientes
    {
        [PrimaryKey]
        [AutoIncrement]
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Comentarios { get; set; }
        public string FotoCliente { get; set; }
    }
}
