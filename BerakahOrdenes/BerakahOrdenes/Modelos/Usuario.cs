using System;
using System.Collections.Generic;

namespace BerakahOrdenes.Modelos
{
    public partial class Usuario
    {
        public Usuario()
        {
            RolMenus = new HashSet<RolMenu>();
            UsuarioRols = new HashSet<UsuarioRol>();
        }

        public int UsuarioId { get; set; }
        public string UsuarioUsuario { get; set; } = null!;
        public byte[] UsuarioPassHash { get; set; } = null!;
        public byte[] UsuarioPassSalt { get; set; } = null!;
        public string UsuarioNombre { get; set; } = null!;
        public string UsuarioApellido { get; set; } = null!;
        public string UsuarioCorreo { get; set; } = null!;
        public string UsuarioTelefono { get; set; } = null!;
        public bool UsuarioEstado { get; set; }
        public DateTime UsuarioFechaSesion { get; set; }
        public DateTime UsaurioFechaCreacion { get; set; }
        public bool? UsuarioCambioPass { get; set; }
        public DateTime UsuarioFechaCambioPass { get; set; }
        public int UsuarioIntentos { get; set; }
        public int UsuarioUsuarioId { get; set; }

        public virtual ICollection<RolMenu> RolMenus { get; set; }
        public virtual ICollection<UsuarioRol> UsuarioRols { get; set; }
    }
}
