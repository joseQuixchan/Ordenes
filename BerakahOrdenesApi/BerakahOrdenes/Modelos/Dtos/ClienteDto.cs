using System;
using System.ComponentModel.DataAnnotations;

namespace BerakahOrdenes.Modelos.Dtos
{
    public class ClienteDto
    {
        public int ClienteId { get; set; }
        [Required(ErrorMessage = "El Nombre es obligatgorio")]
        public string ClienteNombre { get; set; }
        public string ClienteApellido { get; set; }
        public string ClienteTelefono { get; set; }
        [DataType(DataType.EmailAddress)]
        public string ClienteCorreo { get; set; }
        public string ClienteNit { get; set; }
        public string ClienteDireccion { get; set; }
        public bool ClienteEstado { get; set; }
        public DateTime ClienteFechaCreacion { get; set; }
    }

    public class ClienteActualizarDto
    {
        public int ClienteId { get; set; }
    }
}
