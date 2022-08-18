using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BerakahOrdenes.Modelos
{
    public class Menu
    {
        [Key]
        public int MenuId { get; set; }
        public int ModuloId { get; set; }
        [ForeignKey("ModuloId")]
        public Modulo Modulo { get; set; }
        public string MenuNombre { get; set; }
        public string MenuDescripcion { get; set; }
        public string MenuImagen { get; set; }
        public bool MenuEstado { get; set; }
        public DateTime MenuFechaCreacion { get; set; }
    }
}
