using System;
using System.Collections.Generic;

namespace BerakahOrdenes.Modelos
{
    public partial class Tarea
    {
        public int TareaId { get; set; }
        public string TareaDescripcion { get; set; } = null!;
        public bool TareaEstado { get; set; }
        public DateTime TareaFechaCreacion { get; set; }
    }
}
