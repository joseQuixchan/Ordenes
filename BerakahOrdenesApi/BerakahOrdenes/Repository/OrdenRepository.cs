using BerakahOrdenes.Datos;
using BerakahOrdenes.Modelos;
using BerakahOrdenes.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BerakahOrdenes.Repository
{
    public class OrdenRepository : IOrdenRepository
    {
        private readonly DBOrdenes _db;
        public OrdenRepository(DBOrdenes db)
        {
            _db = db;
        }

        public bool ActualizarOrden(Orden orden)
        {
            _db.Orden.Update(orden);
            return Guardar();
        }

        public bool BorrarOrden(Orden orden)
        {
            _db.Orden.Remove(orden);
            return Guardar();
        }

        public bool CrearOrden(Orden orden)
        {
            _db.Orden.Add(orden);
            return Guardar();
        }

        public bool ExisteOrden(int ordenId)
        {
            return _db.Orden.Any(c => c.OrdenId == ordenId);
        }

        public bool ExisteOrdenName(string nombre)
        {
            bool valor = _db.Menu.Any(c => c.MenuNombre.ToLower().Trim().Equals(nombre.ToLower().Trim()));
            return valor;
        }

        public Orden GetOrden(int id)
        {
            return _db.Orden.Include(i => i.OrdenDetalles).FirstOrDefault(c => c.OrdenId == id);
        }

        public ICollection<Orden> GetOrdenes()
        {
            return _db.Orden.OrderByDescending(c => c.OrdenFechaCreacion).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
