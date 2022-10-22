using BerakahOrdenes.Modelos;

namespace BerakahOrdenes.Repository.IRepository
{
    public interface IUsuarioRepository
    {
        ICollection<Usuario> GetUsuarios();
        bool Registro(Usuario usuario, string password);
        Usuario Login(string usuarioUsuario, string usuarioPass);
        Usuario GetUsuario(int id);
        bool ExisteUserName(string userName);
        public bool ExisteCorreo(string correo);
        bool ExisteUsuario(int idUsuario);
        Usuario ExisteUserName2(string userName);
        bool ActualizarUsuario(Usuario usuario);
        bool BorrarUsuario(Usuario usuario);
        bool ActualizarFechaSesionUsuario(Usuario usuario);
        public ICollection<Menu> ObtenerMenusUsuario(int idUsuario, int? idRol);
        bool ActualizarPassword(Usuario usuario, string password);
        bool Guardar();
    }
}
