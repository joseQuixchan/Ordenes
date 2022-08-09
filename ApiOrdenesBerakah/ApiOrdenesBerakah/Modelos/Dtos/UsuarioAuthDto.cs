using System;
using System.ComponentModel.DataAnnotations;

namespace ApiOrdenesBerakah.Modelos.Dtos
{
    public class UsuarioAuthDto
    {
        public int idUsuario { get; set; }
        [Required(ErrorMessage = "El nombre de usuario es obligatgorio")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "El usuario debe tener entre 4 y 5 caracteres")]
        public string userName { get; set; }
        [Required(ErrorMessage = "Es neceario un Password")]
        [StringLength(17, MinimumLength = 12, ErrorMessage = "El usuario debe tener entre 4 y 5 caracteres")]
        public string userPass { get; set; }
        [Required(ErrorMessage = "El nombre es obligatgorio")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "El apelldio es obligatgorio")]
        public string apellido { get; set; }
        [Required(ErrorMessage = "El correo es obligatgorio")]
        public string correo { get; set; }
        [Required(ErrorMessage = "El telefono es obligatgorio")]
        public string telefono { get; set; }
        public int genero { get; set; }
        public DateTime fechaIngreso { get; set; }
    }
}
