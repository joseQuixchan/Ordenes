using BerakahOrdenes.Modelos;

namespace BerakahOrdenes.Repository.IRepository
{
    public interface ITareaRepository
    {
        ICollection<Tarea> GetTareas();
        bool CrearTarea(Tarea tarea);
        Tarea GetTarea(int id);
        bool ExisteTarea(int idCliente);
        bool ActualizarTarea(Tarea tarea);
        bool BorrarTarea(Tarea tarea);
        bool Guardar();
    }
}
