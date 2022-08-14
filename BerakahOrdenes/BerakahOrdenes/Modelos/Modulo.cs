using System;
using System.ComponentModel.DataAnnotations;

namespace BerakahOrdenes.Modelos
{
    public class Modulo
    {
        [Key]
        public int ModuloId { get; set; }
        public string ModuloNombre { get; set; }
        public string ModuloDescripcion { get; set; }
        public string ModuloImagen { get; set; }
        public bool ModuloEstado { get; set; }
        public DateTime ModuloFechaCreacion { get; set; }
    }
}
