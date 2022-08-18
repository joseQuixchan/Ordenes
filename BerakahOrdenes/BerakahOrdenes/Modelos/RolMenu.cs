using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BerakahOrdenes.Modelos
{
    public class RolMenu
    {
        [Key]
        public int RolMenuId { get; set; }
        public int RolId { get; set; }
        [ForeignKey("RolId")]
        public Rol Rol { get; set; }
        public int MenuId { get; set; }
        [ForeignKey("MenuId")]
        public Menu Menu { get; set; }
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }
        public bool Agregar { get; set; }
        public bool Modificar { get; set; }
        public bool Consultar { get; set; }
        public bool Eliminar { get; set; }
        public bool Imprimir { get; set; }
        public bool RolMenuEstado { get; set; }
        public DateTime RolMenuFechaCreacion { get; set; }
    }
}
