using System;
using System.ComponentModel.DataAnnotations;

namespace BerakahOrdenes.Modelos
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; }
        public string ClienteApellido { get; set; }
        public string ClienteTelefono { get; set; }
        public string ClienteCorreo { get; set; }
        public string ClienteNit { get; set; }
        public string ClienteDireccion { get; set; }
        public bool ClienteEstado { get; set; }
        public DateTime ClienteFechaCreacion { get; set; }
    }
}
