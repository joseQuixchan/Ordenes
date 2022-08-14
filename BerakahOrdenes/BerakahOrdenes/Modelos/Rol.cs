using System;
using System.ComponentModel.DataAnnotations;

namespace BerakahOrdenes.Modelos
{
    public class Rol
    {
        [Key]
        public int RolId { get; set; }
        public string RolNombre { get; set; }
        public string RolDescripcion { get; set; }
        public bool RolEstado { get; set; }
        public DateTime RolFechaCreacion { get; set; }
    }
}
