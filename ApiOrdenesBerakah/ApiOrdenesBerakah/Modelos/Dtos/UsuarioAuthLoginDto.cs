using System.ComponentModel.DataAnnotations;

namespace ApiOrdenesBerakah.Modelos.Dtos
{
    public class UsuarioAuthLoginDto
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatgorio")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "El usuario debe tener entre 4 y 5 caracteres")]
        public string userName { get; set; }
        [Required(ErrorMessage = "Es neceario un Password")]
        [StringLength(17, MinimumLength = 12, ErrorMessage = "El usuario debe tener entre 4 y 5 caracteres")]
        public string userPass { get; set; }
    }
}
