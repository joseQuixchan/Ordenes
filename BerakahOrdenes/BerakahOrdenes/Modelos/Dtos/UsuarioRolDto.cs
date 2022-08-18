using System;
using System.ComponentModel.DataAnnotations;

namespace BerakahOrdenes.Modelos.Dtos
{
    public class UsuarioRolDto
    {
        public int UsuarioRolId { get; set; }
        [Required(ErrorMessage = "El Id Usuario es obligatgorio")]
        public int UsuarioId { get; set; }
        [Required(ErrorMessage = "El Id Rol es obligatgorio")]
        public int RolId { get; set; }
        public bool UsuarioRolEstado { get; set; }
        public DateTime UsuarioRolFechaCreacion { get; set; }
    }
}
