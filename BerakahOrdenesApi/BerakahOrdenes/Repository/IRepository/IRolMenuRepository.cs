using BerakahOrdenes.Modelos;

namespace BerakahOrdenes.Repository.IRepository
{
    public interface IRolMenuRepository
    {
        ICollection<RolMenu> GetRolMenus();
        ICollection<RolMenu> GetRolMenusPorRol(int id);
        bool CrearRolMenu(RolMenu rol);
        RolMenu GetRolMenu(int id);
        bool ExisteRolMenuName(string rol);
        bool ExisteRolMenu(int idRol);
        bool ActualizarRolMenu(RolMenu rol);
        bool BorrarRolMenu(RolMenu rol);
        bool Guardar();
    }
}
