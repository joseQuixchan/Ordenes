using BerakahOrdenes.Modelos.Dtos;
using System;
using System.ComponentModel.DataAnnotations;

namespace BerakahOrdenes.Modelos
{
    public class InicioSesion
    {
        public UsuarioPerfilDto Usuario { get; set; }
        
        public object token { get; set; }
    }
}
