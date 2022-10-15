using BerakahOrdenes.Datos;
using BerakahOrdenes.Modelos;
using BerakahOrdenes.Repository.IRepository;

namespace BerakahOrdenes.Repository
{
    public class RolRepository : IRolRepository
    {
        private readonly DBOrdenes _db;
        public RolRepository(DBOrdenes db)
        {
            _db = db;
        }

        public bool ActualizarRol(Rol rol)
        {
            _db.Rol.Update(rol);
            return Guardar();
        }

        public bool BorrarRol(Rol rol)
        {
            _db.Rol.Remove(rol);
            return Guardar();
        }

        public bool CrearRol(Rol rol)
        {
            _db.Rol.Add(rol);
            return Guardar();
        }

        public bool ExisteRol(int rolId)
        {
            return _db.Rol.Any(c => c.RolId == rolId);
        }

        public bool ExisteRolName(string nombre)
        {
            bool valor = _db.Rol.Any(c => c.RolNombre.ToLower().Trim().Equals(nombre.ToLower().Trim()));
            return valor;
        }

        public Rol GetRol(int id)
        {
            return _db.Rol.FirstOrDefault(c => c.RolId == id);
        }

        public ICollection<Rol> GetRoles()
        {
            return _db.Rol.OrderBy(c => c.RolNombre).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
