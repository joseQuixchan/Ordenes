using BerakahOrdenes.Datos;
using BerakahOrdenes.Modelos;
using BerakahOrdenes.Repository.IRepository;

namespace BerakahOrdenes.Repository
{
    public class TareaRepository : ITareaRepository
    {
        private readonly DBOrdenes _db;
        public TareaRepository(DBOrdenes db)
        {
            _db = db;
        }

        public bool ActualizarTarea(Tarea tarea)
        {
            _db.Tarea.Update(tarea);
            return Guardar();
        }

        public bool BorrarTarea(Tarea tarea)
        {
            _db.Tarea.Remove(tarea);
            return Guardar();
        }

        public bool CrearTarea(Tarea tarea)
        {
            
            _db.Tarea.Add(tarea);
            return Guardar();
        }

        public bool ExisteTarea(int idTarea)
        {
            return _db.Tarea.Any(c => c.TareaId == idTarea);
        }

        public Tarea GetTarea(int id)
        {
            return _db.Tarea.FirstOrDefault(c => c.TareaId == id);
        }

        public ICollection<Tarea> GetTareas()
        {
            return _db.Tarea.OrderBy(c => c.TareaId).Where(w => w.TareaEstado == true).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
