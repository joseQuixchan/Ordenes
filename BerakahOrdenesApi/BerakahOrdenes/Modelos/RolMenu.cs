using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BerakahOrdenes.Modelos
{
    public partial class RolMenu
    {
        public int RolMenuId { get; set; }
        public int RolId { get; set; }
        public int MenuId { get; set; }
        public int UsuarioId { get; set; }
        public bool Agregar { get; set; }
        public bool Modificar { get; set; }
        public bool Consultar { get; set; }
        public bool Eliminar { get; set; }
        public bool Imprimir { get; set; }
        public bool RolMenuEstado { get; set; }
        public DateTime RolMenuFechaCreacion { get; set; }

        public virtual Menu Menu { get; set; } = null!;
        public virtual Rol Rol { get; set; } = null!;
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
