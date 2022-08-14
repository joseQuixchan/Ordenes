using BerakahOrdenes.Modelos;

namespace BerakahOrdenes.Repository.IRepository
{
    public interface IClienteRepository
    {
        ICollection<Cliente> GetClientes();
        bool CrearCliente(Cliente cliente);
        Cliente GetCliente(int id);
        bool ExisteClienteName(string nombre);
        public bool ExisteCorreo(string correo);
        bool ExisteCliente(int idCliente);
        bool ActualizarCliente(Cliente cliente);
        bool BorrarCliente(Cliente cliente);
        bool Guardar();
    }
}
