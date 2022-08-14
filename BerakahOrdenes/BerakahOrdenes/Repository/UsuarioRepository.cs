using BerakahOrdenes.Datos;
using BerakahOrdenes.Modelos;
using BerakahOrdenes.Repository.IRepository;

namespace BerakahOrdenes.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DBOrdenes _db;
        public UsuarioRepository(DBOrdenes db)
        {
            _db = db;
        }

        public Usuario Registro(Usuario usuario, string password)
        {
            byte[] passwordHash, passwordSaltl;

            CrearPasswordHash(password, out passwordHash, out passwordSaltl);

            usuario.UsuarioPassHash = passwordHash;
            usuario.UsuarioPassSalt = passwordSaltl;

            _db.Usuario.Add(usuario);
            Guardar();
            return usuario;
        }

        private bool VerificarPasswordHash(string usuarioPass, byte[] usuarioPassHash, byte[] usuarioPassSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(usuarioPassSalt))
            {
                var hashComputado = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(usuarioPass));

                for (int i = 0; i < hashComputado.Length; i++)
                {
                    if (hashComputado[i] != usuarioPassHash[i]) return false;

                }
            }
            return true;
        }

        public Usuario Login(string usuarioUsuario, string usuarioPass)
        {
            var usuario = _db.Usuario.FirstOrDefault(x => x.UsuarioUsuario.Equals(usuarioUsuario));

            if (usuario == null)
            {
                return null;
            }

            if (!VerificarPasswordHash(usuarioPass, usuario.UsuarioPassHash, usuario.UsuarioPassSalt))
            {
                return null;
            }

            return usuario;
        }


        private void CrearPasswordHash(string usuarioPass, out byte[] usuarioPassHash, out byte[] usuarioPassSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                usuarioPassSalt = hmac.Key;
                usuarioPassHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(usuarioPass));
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
            bool valor = _db.Usuario.Any(c => c.UsuarioUsuario.ToLower().Trim().Equals(userName.ToLower().Trim()));
            return valor;
        }

        public bool ExisteCorreo(string correo)
        {
            bool valor = _db.Usuario.Any(c => c.UsuarioCorreo.ToLower().Trim().Equals(correo.ToLower().Trim()));
            return valor;
        }

        public bool ExisteUsuario(int id)
        {
            return _db.Usuario.Any(c => c.UsuarioId == id);
        }

        public Usuario GetUsuario(int id)
        {
            return _db.Usuario.FirstOrDefault(c => c.UsuarioId == id);
        }

        public ICollection<Usuario> GetUsuarios()
        {
            return _db.Usuario.OrderBy(c => c.UsuarioNombre).ToList();
        }

        public bool ActualizarFechaSesionUsuario(Usuario usuario)
        {
            usuario.UsuarioFechaSesion = DateTime.Now;
            _db.Update(usuario);
            return _db.SaveChanges() > 0;
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
