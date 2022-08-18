using System;
using System.ComponentModel.DataAnnotations;

namespace BerakahOrdenes.Modelos.Dtos
{
    public class MenuDto
    {
        public int MenuId { get; set; }
        [Required(ErrorMessage = "El Id Modulo es obligatgorio")]
        public int ModuloId { get; set; }
        public string MenuNombre { get; set; }
        [Required(ErrorMessage = "La Descripcion es obligatgorio")]
        public string MenuDescripcion { get; set; }
        [Required(ErrorMessage = "La imagen es obligatgorio")]
        public string MenuImagen { get; set; }
        public bool MenuEstado { get; set; }
        public DateTime MenuFechaCreacion { get; set; }
    }
}
