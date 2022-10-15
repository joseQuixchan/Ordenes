using BerakahOrdenes.Modelos;

namespace BerakahOrdenes.Repository.IRepository
{
    public interface IUsuarioRolRepository
    {
        ICollection<UsuarioRol> GetUsuarioRoles();
        bool CrearUsuarioRol(UsuarioRol usuarioRol);
        UsuarioRol GetUsuarioRol(int id);
        bool ExisteUsuarioRol(int id);
        bool ActualizarUsuarioRol(UsuarioRol usuarioRol);
        bool BorrarUsuarioRol(UsuarioRol usuarioRol);
        bool Guardar();
    }
}
