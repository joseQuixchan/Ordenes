using BerakahOrdenes.Modelos;

namespace BerakahOrdenes.Repository.IRepository
{
    public interface IProveedorRepository
    {
        ICollection<Proveedor> GetProveedores();
        bool CrearProveedor(Proveedor proveedor);
        Proveedor GetProveedor(int id);
        bool ExisteProveedorName(string nombre);
        public bool ExisteCorreo(string correo);
        bool ExisteProveedor(int idProveedor);
        bool ActualizarProveedor(Proveedor proveedor);
        bool BorrarProveedor(Proveedor proveedor);
        bool Guardar();
    }
}
