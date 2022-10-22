using BerakahOrdenes.Datos;
using BerakahOrdenes.Modelos;
using BerakahOrdenes.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BerakahOrdenes.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly DBOrdenes _db;
        public TokenRepository(DBOrdenes db)
        {
            _db = db;
        }

        public bool ActualizarToken(Token token)
        {
            _db.Token.Update(token);
            return Guardar();
        }

       

        public bool CrearToken(Token token)
        {
            
            _db.Token.Add(token);
            return Guardar();
        }

       

        public Token GetToken(string codigo)
        {
            return _db.Token.Include(i => i.Usuario).Where(w => w.CodigoSeguridad == codigo && w.TokenEstado  == true).FirstOrDefault();
        }

       

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
