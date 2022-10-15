using System;
using System.Collections.Generic;

namespace BerakahOrdenes.Modelos
{
    public partial class OrdenDetalle
    {
        public int OrdenDetalleId { get; set; }
        public int OrdenId { get; set; }
        public int Cantidad { get; set; }
        public string NombreProducto { get; set; } = null!;
        public string? Descripcion { get; set; } = null!;
        public decimal PrecioUniario { get; set; }
        public decimal Total { get; set; }
        public bool OrdenDetalleEstado { get; set; }
        public DateTime OrdenDetalleFechaCreacion { get; set; }

        public virtual Orden? Orden { get; set; } = null!;
    }
}
