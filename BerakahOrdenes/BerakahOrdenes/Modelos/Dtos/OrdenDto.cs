﻿using System;
using System.ComponentModel.DataAnnotations;

namespace BerakahOrdenes.Modelos.Dtos
{
    public class OrdenDto
    {
        public int? OrdenId { get; set; }
        public string ClienteNombre { get; set; }
        public string UsuarioNombre { get; set; }
        public string? ClienteNit { get; set; }
        public string? ClienteCorreo { get; set; }
        public string? ClienteDireccion { get; set; }
        public string? ClienteTelefono { get; set; }
        public List<OrdenDetalle> OrdenDetalles { get; set; }
        public bool OrdenEstado { get; set; }
        public DateTime OrdenFechaEntrega { get; set; }
        public DateTime OrdenFechaCreacion { get; set; }
    }
}