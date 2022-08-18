using BerakahOrdenes.Datos;
using BerakahOrdenes.Modelos;
using BerakahOrdenes.Repository.IRepository;

namespace BerakahOrdenes.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly DBOrdenes _db;
        public MenuRepository(DBOrdenes db)
        {
            _db = db;
        }

        public bool ActualizarMenu(Menu menu)
        {
            _db.Menu.Update(menu);
            return Guardar();
        }

        public bool BorrarMenu(Menu menu)
        {
            _db.Menu.Remove(menu);
            return Guardar();
        }

        public bool CrearMenu(Menu menu)
        {
            _db.Menu.Add(menu);
            return Guardar();
        }

        public bool ExisteMenu(int menuId)
        {
            return _db.Menu.Any(c => c.MenuId == menuId);
        }

        public bool ExisteMenuName(string nombre)
        {
            bool valor = _db.Menu.Any(c => c.MenuNombre.ToLower().Trim().Equals(nombre.ToLower().Trim()));
            return valor;
        }

        public Menu GetMenu(int id)
        {
            return _db.Menu.FirstOrDefault(c => c.MenuId == id);
        }

        public ICollection<Menu> GetMenus()
        {
            return _db.Menu.OrderBy(c => c.MenuNombre).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
