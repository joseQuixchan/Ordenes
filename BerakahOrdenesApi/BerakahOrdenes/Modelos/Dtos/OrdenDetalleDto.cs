using System;
using System.ComponentModel.DataAnnotations;

namespace BerakahOrdenes.Modelos.Dtos
{
    public class OrdenDetalleDto
    {
        public int Cantidad { get; set; }
        public string NombreProducto { get; set; }
        public decimal PrecioUniario { get; set; }
        public string? Descripcion { get; set; }
        public bool OrdenDetalleEstado { get; set; }
        public DateTime OrdenDetalleFechaCreacion { get; set; }
    }
}
