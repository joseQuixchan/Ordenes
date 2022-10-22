using BerakahOrdenes.Modelos;
using Microsoft.EntityFrameworkCore;

namespace BerakahOrdenes.Datos
{
    public class DBOrdenes : DbContext
    {
        public DBOrdenes(DbContextOptions<DBOrdenes> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Modulo> Modulo { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<UsuarioRol> UsuarioRol { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<RolMenu> RolMenu { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Orden> Orden { get; set; }
        public DbSet<OrdenDetalle> OrdenDetalle { get; set; }
        public DbSet<Token> Token { get; set; }
    }
}
