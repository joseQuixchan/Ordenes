using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BerakahOrdenes.Modelos
{
    public class UsuarioRol
    {
        [Key]
        public int UsuarioRolId { get; set; }
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }
        public int RolId { get; set; }
        [ForeignKey("RolId")]
        public Rol Rol { get; set; }
        public bool UsuarioRolEstado { get; set; }
        public DateTime UsuarioRolFechaCreacion { get; set; }
    }
}
