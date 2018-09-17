using System;
using SQLite;

namespace Examen.Modelos
{
    public class Productos
    {
        [PrimaryKey]
        [AutoIncrement]
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int PreciodeVenta { get; set; }
        public int Cantidad { get; set; }
        public int PreciodeCosto { get; set; }
        public string Descripcion { get; set; }
        public string FotoProducto { get; set; }
    }
}
