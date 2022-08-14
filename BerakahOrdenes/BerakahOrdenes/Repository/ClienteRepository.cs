using BerakahOrdenes.Datos;
using BerakahOrdenes.Modelos;
using BerakahOrdenes.Repository.IRepository;

namespace BerakahOrdenes.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DBOrdenes _db;
        public ClienteRepository(DBOrdenes db)
        {
            _db = db;
        }

        public bool ActualizarCliente(Cliente cliente)
        {
            _db.Cliente.Update(cliente);
            return Guardar();
        }

        public bool BorrarCliente(Cliente cliente)
        {
            _db.Cliente.Remove(cliente);
            return Guardar();
        }

        public bool CrearCliente(Cliente cliente)
        {
            _db.Cliente.Add(cliente);
            return Guardar();
        }

        public bool ExisteCliente(int idCliente)
        {
            return _db.Cliente.Any(c => c.ClienteId == idCliente);
        }

        public bool ExisteClienteName(string nombre)
        {
            bool valor = _db.Cliente.Any(c => c.ClienteNombre.ToLower().Trim().Equals(nombre.ToLower().Trim()));
            return valor;
        }

        public bool ExisteCorreo(string correo)
        {
            bool valor = _db.Cliente.Any(c => c.ClienteCorreo.ToLower().Trim().Equals(correo.ToLower().Trim()));
            return valor;
        }

        public Cliente GetCliente(int id)
        {
            return _db.Cliente.FirstOrDefault(c => c.ClienteId == id);
        }

        public ICollection<Cliente> GetClientes()
        {
            return _db.Cliente.OrderBy(c => c.ClienteNombre).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
