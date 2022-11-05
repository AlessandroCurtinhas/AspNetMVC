using Agenda.Data.Repositories;
using Agenda.Presentation.Models;
using Agenda.Presentation.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Agenda.Presentation.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        public IActionResult MinhaConta()
        {
            return View();
        }
        [HttpPost]
        public IActionResult MinhaConta(UsuarioMinhaContaModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["MensagemAlerta"] = "Ocorreram erros no preencheimento do formulário, por favor verifique.";
                return View();
            }

            try
            {
                var auth = UsuarioAutenticadoService.GetUsuarioAutenticado(HttpContext);
                var usuarioRepository = new UsuarioRepository();
                usuarioRepository.UpdateSenha(auth.IdUsuario, model.NovaSenhaConfirmacao);

                TempData["MensagemSucesso"] = "Sua senha foi atualizada com sucesso.";

            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Ocorreu um erro: {e.Message}";
            }
            return View();
        }
    }
}
