using BerakahOrdenes.Datos;
using BerakahOrdenes.Modelos;
using BerakahOrdenes.Repository.IRepository;

namespace BerakahOrdenes.Repository
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly DBOrdenes _db;
        public ProveedorRepository(DBOrdenes db)
        {
            _db = db;
        }

        public bool ActualizarProveedor(Proveedor proveedor)
        {
            _db.Proveedor.Update(proveedor);
            return Guardar();
        }

        public bool BorrarProveedor(Proveedor proveedor)
        {
            _db.Proveedor.Remove(proveedor);
            return Guardar();
        }

        public bool CrearProveedor(Proveedor proveedor)
        {

            _db.Proveedor.Add(proveedor);
            return Guardar();
        }

        public bool ExisteProveedor(int idProveedor)
        {
            return _db.Proveedor.Any(c => c.ProveedorId == idProveedor);
        }

        public bool ExisteProveedorName(string nombre)
        {
            bool valor = _db.Proveedor.Any(c => c.ProveedorNombre.ToLower().Trim().Equals(nombre.ToLower().Trim()));
            return valor;
        }

        public bool ExisteCorreo(string correo)
        {
            bool valor = _db.Cliente.Any(c => c.ClienteCorreo.ToLower().Trim().Equals(correo.ToLower().Trim()));
            return valor;
        }

        public Proveedor GetProveedor(int id)
        {
            return _db.Proveedor.FirstOrDefault(c => c.ProveedorId == id);
        }

        public ICollection<Proveedor> GetProveedores()
        {
            return _db.Proveedor.OrderBy(c => c.ProveedorNombre).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
