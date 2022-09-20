using System;
using System.ComponentModel.DataAnnotations;

namespace BerakahOrdenes.Modelos
{
    public class Proveedor
    {
        [Key]
        public int ProveedorId { get; set; }
        public string ProveedorNombre { get; set; }
        public string ProveedorTelefono { get; set; }
        public string ProveedorCorreo { get; set; }
        public string ProveedorNit { get; set; }
        public string ProveedorDireccion { get; set; }
        public bool ProveedorEstado { get; set; }
        public DateTime ProveedorFechaCreacion { get; set; }
    }
}
