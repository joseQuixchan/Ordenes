using System;
using System.ComponentModel.DataAnnotations;

namespace BerakahOrdenes.Modelos.Dtos
{
    public class ProveedorDto
    {
        public int ProveedorId { get; set; }
        [Required(ErrorMessage = "El Nombre es obligatgorio")]
        public string ProveedorNombre { get; set; }
        [Required(ErrorMessage = "El telefono es obligatgorio")]
        public string ProveedorTelefono { get; set; }
        [DataType(DataType.EmailAddress)]
        public string ProveedorCorreo { get; set; }
        public string ProveedorNit { get; set; }
        public string ProveedorDireccion { get; set; }
        public bool ProveedorEstado { get; set; }
        public DateTime ProveedorFechaCreacion { get; set; }
    }

    public class ProveedorActualizarDto
    {
        public int ProveedorId { get; set; }
    }
}
