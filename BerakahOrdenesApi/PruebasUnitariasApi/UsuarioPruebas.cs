using BerakahOrdenes.Controllers;
using BerakahOrdenes.Repository.IRepository;

namespace PruebasUnitariasApi
{
    public class UsuarioPruebas
    {
        private readonly UsuarioController _usuarioController;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioPruebas(UsuarioController usuarioController, IUsuarioRepository usuarioRepository)
        {
            _usuarioController = usuarioController;
            _usuarioRepository = usuarioRepository;
        }
        [Fact]
        public void Login()
        {
            var result = _usuarioController.Login();
        }
    }
}