using BerakahOrdenes.Modelos;

namespace BerakahOrdenes.Repository.IRepository
{
    public interface IOrdenDetalleRepository
    {
        List<OrdenDetalle> GetOrdenDetalles(int ordenId);
        bool CrearOrdenDetalle(OrdenDetalle orden);
        OrdenDetalle GetOrdenDetalle(int orden);
        bool ExisteOrdenDetalleName(string orden);
        bool ExisteOrdenDetalle(int orden);
        bool ActualizarOrdenDetalle(OrdenDetalle orden);
        bool BorrarOrdenDetalle(OrdenDetalle orden);
        bool Guardar();
    }
}
