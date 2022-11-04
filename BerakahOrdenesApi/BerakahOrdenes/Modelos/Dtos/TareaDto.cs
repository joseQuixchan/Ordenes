using System;
using System.ComponentModel.DataAnnotations;

namespace BerakahOrdenes.Modelos.Dtos
{
    public class TareaDto
    {
        public int TareaId { get; set; }
        public string TareaDescripcion { get; set; } = null!;
        public bool TareaEstado { get; set; }
        public DateTime TareaFechaCreacion { get; set; }
    }

    public class TareaCrearDto
    {
        public string TareaDescripcion { get; set; } = null!;

    }

}
