using BerakahOrdenes.Datos;
using BerakahOrdenes.Modelos;
using BerakahOrdenes.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BerakahOrdenes.Repository
{
    public class RolMenuRepository : IRolMenuRepository
    {
        private readonly DBOrdenes _db;
        public RolMenuRepository(DBOrdenes db)
        {
            _db = db;
        }

        public bool ActualizarRolMenu(RolMenu rolMenu)
        {
            _db.RolMenu.Update(rolMenu);
            return Guardar();
        }

        public bool BorrarRolMenu(RolMenu rolMenu)
        {
            _db.RolMenu.Remove(rolMenu);
            return Guardar();
        }

        public bool CrearRolMenu(RolMenu rolMenu)
        {
            _db.RolMenu.Add(rolMenu);
            return Guardar();
        }

        public bool ExisteRolMenu(int rolMenuId)
        {
            return _db.RolMenu.Any(c => c.RolId == rolMenuId);
        }

        public bool ExisteRolMenuName(string nombre)
        {
            bool valor = _db.Rol.Any(c => c.RolNombre.ToLower().Trim().Equals(nombre.ToLower().Trim()));
            return valor;
        }

        public RolMenu GetRolMenu(int id)
        {
            return _db.RolMenu.Include(i => i.Menu).FirstOrDefault(c => c.RolMenuId == id);
        }

        public ICollection<RolMenu> GetRolMenus()
        {
            return _db.RolMenu.OrderBy(c => c.RolId).ToList();
        }

        public ICollection<RolMenu> GetRolMenusPorRol(int id)
        {
            var rolMenu = _db.RolMenu.Include(i => i.Menu)
                                     .Where(c => c.RolId == id && c.RolMenuEstado == true).ToList();

            return rolMenu;
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
