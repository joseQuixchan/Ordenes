using System;
using System.Collections.Generic;

namespace BerakahOrdenes.Modelos
{
    public partial class Menu
    {
        public Menu()
        {
            RolMenus = new HashSet<RolMenu>();
        }

        public int MenuId { get; set; }
        public int ModuloId { get; set; }
        public string MenuNombre { get; set; } = null!;
        public string MenuDescripcion { get; set; } = null!;
        public string MenuImagen { get; set; } = null!;
        public bool MenuEstado { get; set; }
        public DateTime MenuFechaCreacion { get; set; }

        public virtual Modulo Modulo { get; set; } = null!;
        public virtual ICollection<RolMenu> RolMenus { get; set; }
    }
}
