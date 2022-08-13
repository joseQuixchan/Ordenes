using ApiOrdenesBerakah.Datos;
using ApiOrdenesBerakah.Modelos;
using ApiOrdenesBerakah.Modelos.Dtos;
using ApiOrdenesBerakah.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace ApiOrdenesBerakah.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;
        public UsuarioRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CrearUsuario(Usuario usuario)
        {

            _db.Usuario.Add(usuario);
            return Guardar();
        }

        public Usuario Registro(Usuario usuario, string userPass)
        {
            byte[] passwordHash, passwordSaltl;

            CrearPasswordHash(userPass, out passwordHash, out passwordSaltl);

            usuario.userPassHash = passwordHash; 
            usuario.userPassSalt = passwordSaltl;

            _db.Usuario.Add(usuario);
            Guardar();
            return usuario;
        }

        private bool VerificarPasswordHash(string userPass, byte[] userPassHash, byte[] userPassSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(userPassSalt))
            {
                var hashComputado = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userPass));

                for (int i = 0; i < hashComputado.Length; i++)
                {
                    if (hashComputado[i] != userPassHash[i]) return false;

                }
            }
            return true;
        }

        public Usuario Login(string usuario, string userPass)
        {
            var user = _db.Usuario.FirstOrDefault(x => x.userName == usuario);

            if(user == null)
            {
                return null;
            }

            if(!VerificarPasswordHash(userPass, user.userPassHash, user.userPassSalt))
            {
                return null;
            }

            return user;
        }


        private void CrearPasswordHash (string userPass, out byte[] userPassHash, out byte[] userPassSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                userPassSalt = hmac.Key;
                userPassHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userPass));
            }
        }
        public bool ActualizarUsuario(Usuario usuario)
        {
            _db.Usuario.Update(usuario);
            return Guardar();
        }

        public bool BorrarUsuario(Usuario usuario)
        {
            _db.Usuario.Remove(usuario);
            return Guardar();
        }

        

        public bool ExisteUserName(string userName)
        {
            bool valor = _db.Usuario.Any(c => c.userName.ToLower().Trim() == userName.ToLower().Trim());
            return valor;
        }

        public bool ExisteCorreo(string correo)
        {
            bool valor = _db.Usuario.Any(c => c.correo.ToLower().Trim() == correo.ToLower().Trim());
            return valor;
        }

        public bool ExisteUsuario(int id)
        {
            return _db.Usuario.Any(c => c.idUsuario == id);
        }

        public Usuario GetUsuario(int id)
        {
            return _db.Usuario.FirstOrDefault(c => c.idUsuario == id);
        }

        public ICollection<Usuario> GetUsuarios()
        {
            return _db.Usuario.OrderBy(c => c.nombre).ToList();
        } 

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

    }
}
