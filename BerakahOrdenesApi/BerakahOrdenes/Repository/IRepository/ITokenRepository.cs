using BerakahOrdenes.Modelos;

namespace BerakahOrdenes.Repository.IRepository
{
    public interface ITokenRepository
    {
        bool CrearToken(Token token);
        Token GetToken(string codigo);
        bool ActualizarToken(Token token);
        bool Guardar();
    }
}
