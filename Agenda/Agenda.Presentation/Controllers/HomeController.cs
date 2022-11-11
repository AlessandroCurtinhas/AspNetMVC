using Agenda.Data.Repositories;
using Agenda.Presentation.Service;
using Agenda.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Presentation.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var usuarioAutenticado = UsuarioAutenticadoService.GetUsuarioAutenticado(HttpContext);
            var lista = new List<DashboardModel>();

            try
            {
                var contatoRepository = new ContatoRepository();
                var contatos = contatoRepository.GetAllByUsuario(usuarioAutenticado.IdUsuario);

                if(contatoRepository == null)
                {
                    TempData["MensagemAlerta"] = "Não foi encotrado contatos cadastrados em seu usuario";
                    return View();
                }

                var contatoAmigos = contatos.Count(c => c.Tipo == 1);
                var contatoFamilia = contatos.Count(c => c.Tipo == 2);
                var contatoTrabalho = contatos.Count(c => c.Tipo == 3);
                var contatoOutros = contatos.Count(c => c.Tipo == 4);

                lista.Add(new DashboardModel { TipoContato = "Amigos", Quantidade = contatoAmigos });
                lista.Add(new DashboardModel { TipoContato = "Familia", Quantidade = contatoFamilia });
                lista.Add(new DashboardModel { TipoContato = "Trabalho", Quantidade = contatoTrabalho });
                lista.Add(new DashboardModel { TipoContato = "Outros", Quantidade = contatoOutros });

            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Ocorreu um erro: {e.Message}";
            }

            return View(lista);
        }

    }
}
