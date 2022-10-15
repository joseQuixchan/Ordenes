using System;
using System.Collections.Generic;

namespace BerakahOrdenes.Modelos
{
    public partial class UsuarioRol
    {
        public int UsuarioRolId { get; set; }
        public int UsuarioId { get; set; }
        public int RolId { get; set; }
        public bool UsuarioRolEstado { get; set; }
        public DateTime UsuarioRolFechaCreacion { get; set; }

        public virtual Rol Rol { get; set; } = null!;
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
