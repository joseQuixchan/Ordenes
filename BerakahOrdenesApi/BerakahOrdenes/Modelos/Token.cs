using System;
using System.Collections.Generic;

namespace BerakahOrdenes.Modelos
{
    public partial class Token
    {
        public int TokenId { get; set; }
        public int UsuarioId { get; set; } 
        public string CodigoSeguridad { get; set; } = null!;
        public bool TokenEstado { get; set; }
        public DateTime TokenFechaCreacion { get; set; }

        public virtual Usuario Usuario { get; set; } = null!;
    }
}
