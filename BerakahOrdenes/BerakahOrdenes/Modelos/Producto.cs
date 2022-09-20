using System;
using System.ComponentModel.DataAnnotations;

namespace BerakahOrdenes.Modelos
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; }
        public string ProductoDescripcion { get; set; }
        public bool ProductoEstado { get; set; }
        public DateTime ProductoFechaCreacion { get; set; }
    }
}
