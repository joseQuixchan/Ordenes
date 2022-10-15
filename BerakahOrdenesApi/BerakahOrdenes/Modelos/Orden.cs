using System;
using System.Collections.Generic;

namespace BerakahOrdenes.Modelos
{
    public partial class Orden
    {
        public Orden()
        {
            OrdenDetalles = new HashSet<OrdenDetalle>();
        }

        public int OrdenId { get; set; }
        public string ClienteNombre { get; set; } = null!;
        public string UsuarioNombre { get; set; } = null!;
        public bool OrdenEstado { get; set; }
        public DateTime OrdenFechaCreacion { get; set; }
        public DateTime OrdenFechaEntrega { get; set; }
        public string? ClienteNit { get; set; }
        public string? ClienteCorreo { get; set; }
        public string? ClienteDireccion { get; set; }
        public string? ClienteTelefono { get; set; }

        public virtual ICollection<OrdenDetalle> OrdenDetalles { get; set; }
    }
}
