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

        public IActionResult Password()
        {
            return View();
        }
    }
}
