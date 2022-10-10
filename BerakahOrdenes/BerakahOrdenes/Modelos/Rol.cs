using System;
using System.Collections.Generic;

namespace BerakahOrdenes.Modelos
{
    public partial class Rol
    {
        public Rol()
        {
            RolMenus = new HashSet<RolMenu>();
            UsuarioRols = new HashSet<UsuarioRol>();
        }

        public int RolId { get; set; }
        public string RolNombre { get; set; } = null!;
        public string RolDescripcion { get; set; } = null!;
        public bool RolEstado { get; set; }
        public DateTime RolFechaCreacion { get; set; }

        public virtual ICollection<RolMenu> RolMenus { get; set; }
        public virtual ICollection<UsuarioRol> UsuarioRols { get; set; }
    }
}
