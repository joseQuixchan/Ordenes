using BerakahOrdenes.Modelos;

namespace BerakahOrdenes.Repository.IRepository
{
    public interface IRolRepository
    {
        ICollection<Rol> GetRoles();
        bool CrearRol(Rol rol);
        Rol GetRol(int id);
        bool ExisteRolName(string rol);
        bool ExisteRol(int idRol);
        bool ActualizarRol(Rol rol);
        bool BorrarRol(Rol rol);
        bool Guardar();
    }
}
