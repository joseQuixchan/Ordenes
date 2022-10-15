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
        [Required(ErrorMessage = "El correo es obligatgorio")]
        public string ProveedorCorreo { get; set; }
        [Required(ErrorMessage = "El correo es obligatgorio")]
        public string ProveedorNit { get; set; }
        public string ProveedorDireccion { get; set; }
        public bool ProveedorEstado { get; set; }
        public DateTime ProveedorFechaCreacion { get; set; }
    }
}
