using ApiOrdenesBerakah.Datos;
using ApiOrdenesBerakah.Modelos;
using ApiOrdenesBerakah.Modelos.Dtos;
using ApiOrdenesBerakah.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace ApiOrdenesBerakah.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _db;
        public ClienteRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CrearCliente(Cliente cliente)
        {

            _db.Cliente.Add(cliente);
            return Guardar();
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


        public bool ExisteCliente(int id)
        {
            return _db.Cliente.Any(c => c.idCliente == id);
        }

        public Cliente GetCliente(int id)
        {
            return _db.Cliente.FirstOrDefault(c => c.idCliente == id);
        }

        public ICollection<Cliente> GetClientes()
        {
            return _db.Cliente.OrderBy(c => c.nombre).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

    }
}
