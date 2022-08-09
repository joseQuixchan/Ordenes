using ApiOrdenesBerakah.Modelos;
using ApiOrdenesBerakah.Modelos.Dtos;
using System.Collections.Generic;

namespace ApiOrdenesBerakah.Repository.IRepository
{
    public interface IClienteRepository
    {
        ICollection<Cliente> GetClientes();
        Cliente GetCliente(int id);
        bool ExisteCliente(int idCliente);
        bool CrearCliente(Cliente cliente);
        bool ActualizarCliente(Cliente cliente);
        bool BorrarCliente(Cliente cliente);
        bool Guardar();
        

    }
}
