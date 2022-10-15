using BerakahOrdenes.Datos;
using BerakahOrdenes.Modelos;
using BerakahOrdenes.Repository.IRepository;

namespace BerakahOrdenes.Repository
{
    public class OrdenDetalleRepository : IOrdenDetalleRepository
    {
        private readonly DBOrdenes _db;
        public OrdenDetalleRepository(DBOrdenes db)
        {
            _db = db;
        }

        public bool ActualizarOrdenDetalle(OrdenDetalle orden)
        {
            _db.OrdenDetalle.Update(orden);
            return Guardar();
        }

        public bool BorrarOrdenDetalle(OrdenDetalle orden)
        {
            _db.OrdenDetalle.Remove(orden);
            return Guardar();
        }

        public bool CrearOrdenDetalle(OrdenDetalle orden)
        {
            _db.OrdenDetalle.Add(orden);
            return Guardar();
        }

        public bool ExisteOrdenDetalle(int ordenId)
        {
            return _db.OrdenDetalle.Any(c => c.OrdenDetalleId == ordenId);
        }

        public bool ExisteOrdenDetalleName(string nombre)
        {
            bool valor = _db.Menu.Any(c => c.MenuNombre.ToLower().Trim().Equals(nombre.ToLower().Trim()));
            return valor;
        }

        public OrdenDetalle GetOrdenDetalle(int id)
        {
            return _db.OrdenDetalle.FirstOrDefault(c => c.OrdenDetalleId == id);
        }

        public List<OrdenDetalle> GetOrdenDetalles(int ordenId)
        {
            return _db.OrdenDetalle.Where(c => c.OrdenId == ordenId).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
