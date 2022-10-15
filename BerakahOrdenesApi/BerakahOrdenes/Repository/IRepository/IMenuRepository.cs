using BerakahOrdenes.Modelos;

namespace BerakahOrdenes.Repository.IRepository
{
    public interface IMenuRepository
    {
        ICollection<Menu> GetMenus();
        bool CrearMenu(Menu menu);
        Menu GetMenu(int id);
        bool ExisteMenuName(string nombre);
        bool ExisteMenu(int id);
        bool ActualizarMenu(Menu menu);
        bool BorrarMenu(Menu menu);
        bool Guardar();
    }
}
