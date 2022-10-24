using BerakahOrdenes.Datos;
using BerakahOrdenes.Modelos;
using BerakahOrdenes.Repository.IRepository;

namespace BerakahOrdenes.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly DBOrdenes _db;
        public ProductoRepository(DBOrdenes db)
        {
            _db = db;
        }

        public bool ActualizarProducto(Producto producto)
        {
            _db.Producto.Update(producto);
            return Guardar();
        }

        public bool BorrarProducto(Producto producto)
        {
            _db.Producto.Remove(producto);
            return Guardar();
        }

        public bool CrearProducto(Producto producto)
        {
            
            _db.Producto.Add(producto);
            return Guardar();
        }

        public bool ExisteProducto(int idProducto)
        {
            return _db.Producto.Any(c => c.ProductoId == idProducto);
        }

        public bool ExisteProductoName(string nombre)
        {
            bool valor = _db.Producto.Any(c => c.ProductoNombre.ToLower().Trim().Equals(nombre.ToLower().Trim()));
            return valor;
        }

        public bool ExisteCorreo(string correo)
        {
            bool valor = _db.Cliente.Any(c => c.ClienteCorreo.ToLower().Trim().Equals(correo.ToLower().Trim()));
            return valor;
        }

        public Producto GetProducto(int id)
        {
            return _db.Producto.FirstOrDefault(c => c.ProductoId == id);
        }

        public ICollection<Producto> GetProductos()
        {
            return _db.Producto.OrderBy(c => c.ProductoNombre).Where(w => w.ProductoEstado == true).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
