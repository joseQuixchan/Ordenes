using System;
using System.ComponentModel.DataAnnotations;

namespace BerakahOrdenes.Modelos.Dtos
{
    public class UsuarioAuthDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de usuario es obligatgorio")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "El usuario debe tener entre 4 y 5 caracteres")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Es neceario un Password")]
        [StringLength(17, MinimumLength = 4, ErrorMessage = "El Password debe tener entre 4 y 5 caracteres")]
        public string UsuarioPass { get; set; }
        [Required(ErrorMessage = "El nombre es obligatgorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apelldio es obligatgorio")]
        public string Apellido { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "El correo es obligatgorio")]
        public string Correo { get; set; }
        [Required(ErrorMessage = "El telefono es obligatgorio")]
        public string Telefono { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaSesion { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
