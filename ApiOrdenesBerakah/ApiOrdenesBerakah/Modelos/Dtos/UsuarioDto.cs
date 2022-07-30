using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiOrdenesBerakah.Modelos.Dtos
{
    public class UsuarioDto
    {
    
        public int idUsuario { get; set; }
        [Required(ErrorMessage = "El nombre de usuario es obligatgorio")]
        public string userName { get; set; }
        [Required(ErrorMessage = "Es neceario un Password")]
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
        
    }
}
