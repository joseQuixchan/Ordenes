using BerakahOrdenes.Datos;
using BerakahOrdenes.Modelos;
using BerakahOrdenes.Repository.IRepository;

namespace BerakahOrdenes.Repository
{
    public class ModuloRepository : IModuloRepository
    {
        private readonly DBOrdenes _db;
        public ModuloRepository(DBOrdenes db)
        {
            _db = db;
        }

        public bool ActualizarModulo(Modulo modulo)
        {
            _db.Modulo.Update(modulo);
            return Guardar();
        }

        public bool BorrarModulo(Modulo modulo)
        {
            _db.Modulo.Remove(modulo);
            return Guardar();
        }

        public bool CrearModulo(Modulo modulo)
        {
            _db.Modulo.Add(modulo);
            return Guardar();
        }

        public bool ExisteModulo(int moduloId)
        {
            return _db.Modulo.Any(c => c.ModuloId == moduloId);
        }

        public bool ExisteModuloName(string nombre)
        {
            bool valor = _db.Modulo.Any(c => c.ModuloNombre.ToLower().Trim().Equals(nombre.ToLower().Trim()));
            return valor;
        }

        public Modulo GetModulo(int id)
        {
            return _db.Modulo.FirstOrDefault(c => c.ModuloId == id);
        }

        public ICollection<Modulo> GetModulos()
        {
            return _db.Modulo.OrderBy(c => c.ModuloNombre).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
