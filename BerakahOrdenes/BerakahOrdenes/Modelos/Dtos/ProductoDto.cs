using System;
using System.ComponentModel.DataAnnotations;

namespace BerakahOrdenes.Modelos.Dtos
{
    public class ProductoDto
    {
        public int ProductoId { get; set; }
        [Required(ErrorMessage = "El Nombre es obligatgorio")]
        public string ProductoNombre { get; set; }
        [Required(ErrorMessage = "La Descripcion es requerida obligatgorio")]
        public string ProductoDescripcion { get; set; }
        public bool ProductoEstado { get; set; }
        public DateTime ProductoFechaCreacion { get; set; }
    }
}
