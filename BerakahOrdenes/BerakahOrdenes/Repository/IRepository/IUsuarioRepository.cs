using BerakahOrdenes.Modelos;

namespace BerakahOrdenes.Repository.IRepository
{
    public interface IUsuarioRepository
    {
        ICollection<Usuario> GetUsuarios();
        Usuario Registro(Usuario usuario, string password);
        Usuario Login(string usuarioUsuario, string usuarioPass);
        Usuario GetUsuario(int id);
        bool ExisteUserName(string userName);
        public bool ExisteCorreo(string correo);
        bool ExisteUsuario(int idUsuario);
        bool ActualizarUsuario(Usuario usuario);
        bool BorrarUsuario(Usuario usuario);
        bool Guardar();
    }
}
