using System;
using System.Collections.Generic;

namespace BerakahOrdenes.Modelos
{
    public partial class Modulo
    {
        public Modulo()
        {
            Menus = new HashSet<Menu>();
        }

        public int ModuloId { get; set; }
        public string ModuloNombre { get; set; } = null!;
        public string ModuloDescripcion { get; set; } = null!;
        public string ModuloImagen { get; set; } = null!;
        public bool ModuloEstado { get; set; }
        public DateTime ModuloFechaCreacion { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }
    }
}
