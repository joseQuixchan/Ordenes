using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiOrdenesBerakah.Modelos.Dtos
{
    public class ClienteDto
    {

        [Key]
        public int idCliente { get; set; }
        public string nombre { get; set; }
        public string apellidio { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public int genero { get; set; }
        public DateTime fechaIngreso { get; set; }
    }
}
