using BerakahOrdenes.Modelos;

namespace BerakahOrdenes.Repository.IRepository
{
    public interface IOrdenRepository
    {
        ICollection<Orden> GetOrdenesFehca();
        ICollection<Orden> GetOrdenes();
        bool CrearOrden(Orden orden);
        Orden GetOrden(int orden);
        bool ExisteOrdenName(string orden);
        bool ExisteOrden(int orden);
        bool ActualizarOrden(Orden orden);
        bool BorrarOrden(Orden orden);
        bool Guardar();
    }
}
