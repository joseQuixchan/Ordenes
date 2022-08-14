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
    }
}
