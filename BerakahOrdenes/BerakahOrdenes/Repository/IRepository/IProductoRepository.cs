using BerakahOrdenes.Modelos;

namespace BerakahOrdenes.Repository.IRepository
{
    public interface IProductoRepository
    {
        ICollection<Producto> GetProductos();
        bool CrearProducto(Producto producto);
        Producto GetProducto(int id);
        bool ExisteProductoName(string nombre);
        public bool ExisteCorreo(string correo);
        bool ExisteProducto(int idProducto);
        bool ActualizarProducto(Producto producto);
        bool BorrarProducto(Producto producto);
        bool Guardar();
    }
}
