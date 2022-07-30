using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiOrdenesBerakah.Modelos
{
    public class Usuario
    {
        [Key]
        public int idUsuario { get; set; }
        public string userName { get; set; }
        public string userPass { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public int genero { get; set; }
        public DateTime fechaIngreso { get; set; }
    }
}
