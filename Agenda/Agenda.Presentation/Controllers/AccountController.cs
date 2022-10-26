using Agenda.Data.Entities;
using Agenda.Data.Repositories;
using Agenda.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Presentation.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AccountRegisterModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var usuario = new Usuario
                    {
                        IdUsuario = Guid.NewGuid(),
                        Nome = model.Nome,
                        Email = model.Email,
                        Senha = model.Senha,
                        DataCriacao = DateTime.Now
                    };

                    var usuarioRepository = new UsuarioRepository();
                    usuarioRepository.Create(usuario);

                    TempData["Mensagem"] = $"Parabéns {usuario.Nome}, sua conta foi criada com sucesso!";
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = $"Erro: {e.Message}";
                }
            }
            
            return View();
        }

        public IActionResult Password()
        {
            return View();
        }
    }
}
