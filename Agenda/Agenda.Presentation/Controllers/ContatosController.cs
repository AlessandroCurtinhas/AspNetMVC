using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Presentation.Controllers
{
    [Authorize]
    public class ContatosController : Controller
    {
        public IActionResult Cadastro()
        {
            return View();
        }
        public IActionResult Consulta()
        {
            return View();
        }
        public IActionResult Edicao()
        {
            return View();
        }
        public IActionResult Relatorio()
        {
            return View();
        }
    }
}
