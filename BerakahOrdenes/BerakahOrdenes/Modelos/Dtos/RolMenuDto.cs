using System;
using System.ComponentModel.DataAnnotations;

namespace BerakahOrdenes.Modelos.Dtos
{
    public class RolMenuDto
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
    }
}
