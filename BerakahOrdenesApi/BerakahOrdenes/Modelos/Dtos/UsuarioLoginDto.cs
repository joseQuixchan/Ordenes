using System;
using System.ComponentModel.DataAnnotations;

namespace BerakahOrdenes.Modelos.Dtos
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatgorio")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "No se ingreso ninguna contrania")]
        public string UsuarioPass { get; set; }
    }
}
