using System;
using System.ComponentModel.DataAnnotations;

namespace BerakahOrdenes.Modelos
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        public string UsuarioUsuario { get; set; }
        public byte[] UsuarioPassHash { get; set; }
        public byte[] UsuarioPassSalt { get; set; }
        public string UsuarioNombre { get; set; }
        public string UsuarioApellido { get; set; }
        public string UsuarioCorreo { get; set; }
        public string UsuarioTelefono { get; set; }
        public bool UsuarioEstado { get; set; }
        public DateTime UsuarioFechaSesion { get; set; }
        public DateTime UsaurioFechaCreacion { get; set; }
    }
}
