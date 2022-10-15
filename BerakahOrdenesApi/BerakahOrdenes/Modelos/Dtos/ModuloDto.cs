using System;
using System.ComponentModel.DataAnnotations;

namespace BerakahOrdenes.Modelos.Dtos
{
    public class ModuloDto
    {
        public int ModuloId { get; set; }
        [Required(ErrorMessage = "El Nombre es obligatgorio")]
        public string ModuloNombre { get; set; }
        [Required(ErrorMessage = "La Descripcion es obligatgorio")]
        public string ModuloDescripcion { get; set; }
        [Required(ErrorMessage = "La imagen es obligatgorio")]
        public string ModuloImagen { get; set; }
        public bool ModuloEstado { get; set; }
        public DateTime ModuloFechaCreacion { get; set; }
    }
}
