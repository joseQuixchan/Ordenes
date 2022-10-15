using System;
using System.ComponentModel.DataAnnotations;

namespace BerakahOrdenes.Modelos.Dtos
{
    public class ClienteDto
    {
        public int ClienteId { get; set; }
        [Required(ErrorMessage = "El Nombre es obligatgorio")]
        public string ClienteNombre { get; set; }
        [Required(ErrorMessage = "El apellido es obligatgorio")]
        public string ClienteApellido { get; set; }
        [Required(ErrorMessage = "El telefono es obligatgorio")]
        public string ClienteTelefono { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "El correo es obligatgorio")]
        public string ClienteCorreo { get; set; }
        [Required(ErrorMessage = "El correo es obligatgorio")]
        public string ClienteNit { get; set; }
        public string ClienteDireccion { get; set; }
        public bool ClienteEstado { get; set; }
        public DateTime ClienteFechaCreacion { get; set; }
    }
}
