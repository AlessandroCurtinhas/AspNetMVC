using Agenda.Presentation.Models;
using Newtonsoft.Json;

namespace Agenda.Presentation.Service
{
    public class UsuarioAutenticadoService
    {
        public static AuthenticationModel GetUsuarioAutenticado(HttpContext contexto)
        {
            var json = contexto.User.Identity.Name;
            var usuarioAutenticado = JsonConvert.DeserializeObject<AuthenticationModel>(json);

            return usuarioAutenticado;
        }
    }
}
