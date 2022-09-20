using System;
using System.ComponentModel.DataAnnotations;

namespace BerakahOrdenes.Modelos.Dtos
{
    public class UsuarioDto
    {
        public int UsuarioId { get; set; }
        [Required(ErrorMessage = "El nombre de usuario es obligatgorio")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "El usuario debe tener entre 4 y 5 caracteres")]
        public string UsuarioUsuario { get; set; }
        public byte[] UsuarioPassHash { get; set; }
        [Required(ErrorMessage = "El nombre es obligatgorio")]
        public string UsuarioNombre { get; set; }
        [Required(ErrorMessage = "El apelldio es obligatgorio")]
        public string UsuarioApellido { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "El correo es obligatgorio")]
        public string UsuarioCorreo { get; set; }
        [Required(ErrorMessage = "El telefono es obligatgorio")]
        public string UsuarioTelefono { get; set; }
        public bool UsuarioEstado { get; set; }
        public DateTime UsuarioFechaSesion { get; set; }
        public DateTime UsaurioFechaCreacion { get; set; }
    }

    public class UsuarioActualizarDto
    {
        public int UsuarioId { get; set; }
        [Required(ErrorMessage = "El nombre de usuario es obligatgorio")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "El usuario debe tener entre 4 y 5 caracteres")]
        public string UsuarioUsuario { get; set; }
        [Required(ErrorMessage = "El nombre es obligatgorio")]
        public string UsuarioNombre { get; set; }
        [Required(ErrorMessage = "El apelldio es obligatgorio")]
        public string UsuarioApellido { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "El correo es obligatgorio")]
        public string UsuarioCorreo { get; set; }
        [Required(ErrorMessage = "El telefono es obligatgorio")]
        public string UsuarioTelefono { get; set; }
        public bool UsuarioEstado { get; set; }
        public DateTime UsuarioFechaSesion { get; set; }
        public DateTime UsaurioFechaCreacion { get; set; }
    }
}
