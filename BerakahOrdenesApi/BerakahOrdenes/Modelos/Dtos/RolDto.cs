using System;
using System.ComponentModel.DataAnnotations;

namespace BerakahOrdenes.Modelos.Dtos
{
    public class RolDto
    {
        public int RoloId { get; set; }
        [Required(ErrorMessage = "El Nombre es obligatgorio")]
        public string RolNombre { get; set; }
        [Required(ErrorMessage = "La Descripcion es obligatgorio")]
        public string RolDescripcion { get; set; }
        public bool RolEstado { get; set; }
        public DateTime RolFechaCreacion { get; set; }
    }
}
