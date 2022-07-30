using ApiOrdenesBerakah.Modelos;
using ApiOrdenesBerakah.Modelos.Dtos;
using System.Collections.Generic;

namespace ApiOrdenesBerakah.Repository.IRepository
{
    public interface IUsuarioRepository
    {
        ICollection<Usuario> GetUsuarios();
        Usuario GetUsuario(int id);
        bool ExisteUserName(string userName);
        public bool ExisteCorreo(string correo);
        bool ExisteUsuario(int idUsuario);
        bool CrearUsuario(Usuario usuario);
        bool ActualizarUsuario(Usuario usuario);
        bool BorrarUsuario(Usuario usuario);
        bool Guardar();
        

    }
}
