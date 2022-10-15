using BerakahOrdenes.Modelos;

namespace BerakahOrdenes.Repository.IRepository
{
    public interface IModuloRepository
    {
        ICollection<Modulo> GetModulos();
        bool CrearModulo(Modulo modulo);
        Modulo GetModulo(int id);
        bool ExisteModuloName(string nombre);
        bool ExisteModulo(int idCliente);
        bool ActualizarModulo(Modulo modulo);
        bool BorrarModulo(Modulo modulo);
        bool Guardar();
    }
}
