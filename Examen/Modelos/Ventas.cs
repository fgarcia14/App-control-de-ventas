using System;
using SQLite;

namespace Examen.Modelos
{
    public class Ventas
    {
        [PrimaryKey]
        [AutoIncrement]
        public int IdVenta { get; set; }
        public int Cliente { get; set; }
        public int Producto { get; set; }
        public DateTime Fecha { get; set; }
        public string Pagado { get; set; }
    }
}
