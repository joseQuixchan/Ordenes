using System;
using System.Collections.Generic;

namespace BerakahOrdenes.Modelos
{
    public partial class Producto
    {
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; } = null!;
        public string ProductoDescripcion { get; set; } = null!;
        public bool ProductoEstado { get; set; }
        public DateTime ProductoFechaCreacion { get; set; }
    }
}
