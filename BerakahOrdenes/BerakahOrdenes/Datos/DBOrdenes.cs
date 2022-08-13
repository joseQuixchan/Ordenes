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
    }
}
