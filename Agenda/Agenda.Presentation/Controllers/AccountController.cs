using Agenda.Data.Entities;
using Agenda.Data.Repositories;
using Agenda.Messages.Services;
using Agenda.Presentation.Models;
using Bogus;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Agenda.Presentation.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(AccountLoginModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var usuarioRepository = new UsuarioRepository();
                    var usuario = usuarioRepository.GetByEmailAndSenha(model.Email, model.Senha);

                    if(usuario == null)
                    {
                        ModelState.Clear();
                        throw new Exception($"Usuario ou senha inválidos.");
                    }

                    var authenticationModel = new AuthenticationModel
                    {
                        IdUsuario = usuario.IdUsuario,
                        Nome = usuario.Nome,
                        Email = usuario.Email,
                        DataHoraAcesso = DateTime.Now
                    };

                    //gerar conteudo para gravação no cookie de autenticação
                    var json = JsonConvert.SerializeObject(authenticationModel);
                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, json) }, CookieAuthenticationDefaults.AuthenticationScheme);

                    //gravando cookie
                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = $"Erro: {e.Message}";
                }
            }
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
                    var usuarioRepository = new UsuarioRepository();
                    var usuarioEmail = usuarioRepository.GetByEmail(model.Email);

                    if (usuarioEmail != null)
                    {
                        ModelState.Clear();
                        throw new Exception($"O Email {usuarioEmail.Email} já está cadastrado para outro usuário");
                    }
                        
                    var usuario = new Usuario
                    {
                        IdUsuario = Guid.NewGuid(),
                        Nome = model.Nome,
                        Email = model.Email,
                        Senha = model.Senha,
                        DataCriacao = DateTime.Now
                    };
                 
                    usuarioRepository.Create(usuario);

                    TempData["Mensagem"] = $"Parabéns {usuario.Nome}, sua conta foi criada com sucesso!";

                    ModelState.Clear();
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

        [HttpPost]
        public IActionResult Password(AccountPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioRepository = new UsuarioRepository();
                    var usuario = usuarioRepository.GetByEmail(model.Email);

                    if(usuario == null)
                    {
                        throw new Exception("Email inválido");
                    }

                    #region NovaSenha
                    var faker = new Faker();
                    var novaSenha = $"@{faker.Internet.Password()}";
                    #endregion

                    #region Envio do Email
                    var emailMessageService = new EmailMessageService();
                    var subject = "Recuperação de Senha - Agenda de Contatos";
                    var body = @$"
                                <h3>Olá {usuario.Nome}</h3>
                                <p> Uma nova senha gerada com uscesso para o seu usuário. </p>
                                <p> Acessa sua agenda com a senha: <strong>{novaSenha}</strong></p>
                                <p> Após acessar a agenda, você poderpa utilizar o menu 'Minha Conta' para alterar sua senha. </p>
                                <br/>
                                <p>Att, <br/> Equipe Agenda de Contatos </p>
                        ";

                    emailMessageService.SendMessage(usuario.Email, subject, body);
                    usuarioRepository.UpdateSenha(usuario.IdUsuario, novaSenha);
                    
                    TempData["Mensagem"] = $"A senha nova foi enviada para seu email.";
                    ModelState.Clear();

                    return Redirect("Login");

                    #endregion
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = $"Erro: {e.Message}";
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
