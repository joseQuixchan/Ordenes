using BerakahOrdenes.Datos;
using BerakahOrdenes.Modelos;
using BerakahOrdenes.Repository.IRepository;

namespace BerakahOrdenes.Repository
{
    public class UsuarioRolRepository : IUsuarioRolRepository
    {
        private readonly DBOrdenes _db;
        public UsuarioRolRepository(DBOrdenes db)
        {
            _db = db;
        }

        public bool ActualizarUsuarioRol(UsuarioRol usuarioRol)
        {
            _db.UsuarioRol.Update(usuarioRol);
            return Guardar();
        }

        public bool BorrarUsuarioRol(UsuarioRol usuarioRol)
        {
            _db.UsuarioRol.Remove(usuarioRol);
            return Guardar();
        }

        public bool CrearUsuarioRol(UsuarioRol usuarioRol)
        {
            _db.UsuarioRol.Add(usuarioRol);
            return Guardar();
        }

        public bool ExisteUsuarioRol(int id)
        {
            return _db.UsuarioRol.Any(c => c.UsuarioRolId == id);
        }

        public UsuarioRol GetUsuarioRol(int id)
        {
            return _db.UsuarioRol.FirstOrDefault(c => c.UsuarioRolId == id);
        }

        public ICollection<UsuarioRol> GetUsuarioRoles()
        {
            return _db.UsuarioRol.OrderBy(c => c.UsuarioRolId).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
