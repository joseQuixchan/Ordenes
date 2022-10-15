using System;
using System.Collections.Generic;

namespace BerakahOrdenes.Modelos
{
    public partial class Proveedor
    {
        public int ProveedorId { get; set; }
        public string ProveedorNombre { get; set; } = null!;
        public string ProveedorTelefono { get; set; } = null!;
        public string ProveedorCorreo { get; set; } = null!;
        public string ProveedorNit { get; set; } = null!;
        public string ProveedorDireccion { get; set; } = null!;
        public bool ProveedorEstado { get; set; }
        public DateTime ProveedorFechaCreacion { get; set; }
    }
}
