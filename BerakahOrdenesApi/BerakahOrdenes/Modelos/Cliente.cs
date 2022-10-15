using System;
using System.Collections.Generic;

namespace BerakahOrdenes.Modelos
{
    public partial class Cliente
    {
        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; } = null!;
        public string ClienteApellido { get; set; } = null!;
        public string ClienteTelefono { get; set; } = null!;
        public string ClienteCorreo { get; set; } = null!;
        public string ClienteNit { get; set; } = null!;
        public bool ClienteEstado { get; set; }
        public DateTime ClienteFechaCreacion { get; set; }
        public string ClienteDireccion { get; set; } = null!;
    }
}
