using Agenda.Data.Entities;
using Agenda.Data.Repositories;
using Agenda.Presentation.Models;
using Agenda.Presentation.Auxiliar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Agenda.Reports.Interfaces;
using Agenda.Reports.Services;

namespace Agenda.Presentation.Controllers
{
    [Authorize]
    public class ContatosController : Controller
    {
        public IActionResult Cadastro()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastro(ContatoCadastroModel model)
        {
            if(!ModelState.IsValid)
            {
                TempData["MensagemAlerta"] = "Ocorreram erros no preechimento do formulário de cadastro, por favor, verifique";
                return View();
            }
            try
            {
                var usuarioAutenticado = UsuarioAutenticadoService.GetUsuarioAutenticado(HttpContext);
                var contatoRepository = new ContatoRepository();

                var contato = new Contato
                {
                    IdContato = Guid.NewGuid(),
                    Nome = model.Nome,
                    Email = model.Email,
                    Telefone = model.Telefone,
                    DataNascimento = DateTime.Parse(model.DataNascimento),
                    Tipo = int.Parse(model.Tipo),
                    IdUsuario = usuarioAutenticado.IdUsuario
                };

                contatoRepository.Create(contato);
                TempData["MensagemSucesso"] = $"Contato {contato.Nome} cadastrado com sucesso.";
                ModelState.Clear();  
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Ocorreu um erro: {e.Message}";
            }
            return View();
        }
        public IActionResult Consulta()
        {
            try
            {
                var listaContatos = new List<ContatoConsultaModel>();
                var usuarioAutenticado = UsuarioAutenticadoService.GetUsuarioAutenticado(HttpContext);
                var contatoRepository = new ContatoRepository();
                var contatos = contatoRepository.GetAllByUsuario(usuarioAutenticado.IdUsuario);
                if (contatos == null) 
                { 
                    TempData["MensagemAltera"] = "Nenhum contato foi encontrado!";
                    return View();
                }

                foreach (var item in contatos)
                {
                    var model = new ContatoConsultaModel
                    {
                        IdContato = item.IdContato,
                        Nome = item.Nome,
                        Telefone = item.Telefone,
                        Email = item.Email,
                        DataNascimento = item.DataNascimento.ToString("dd/MM/yyyy"),
                        Tipo = item.Tipo == 1 ? "Amigos" : item.Tipo == 2 ? "Trabalho" : item.Tipo == 3 ? "Família" : "Outros"
                    };
                    listaContatos.Add(model);
                }
                return View(listaContatos);
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Ocorreu um erro: {e.Message}";
            }

            return View();
        }

        public IActionResult Edicao(Guid id)
        {
            try
            {
                var contatoRepository = new ContatoRepository();
                var contato = contatoRepository.GetById(id);
                var usuarioAutenticado = UsuarioAutenticadoService.GetUsuarioAutenticado(HttpContext);

                if (contato == null || contato.IdUsuario != usuarioAutenticado.IdUsuario)
                {
                    TempData["MensagemAltera"] = "Nenhum contato foi encontrado!";
                    return RedirectToAction("Consulta");
                }

                var contatoEdicaoModel = new ContatoEdicaoModel
                {
                    IdContato = contato.IdContato,
                    Nome = contato.Nome,
                    Telefone = contato.Telefone,
                    Email = contato.Email,
                    DataNascimento = contato.DataNascimento.ToString("dd/MM/yyyy"),
                    Tipo = contato.Tipo.ToString()
                };
                
                return View(contatoEdicaoModel);

            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Ocorreu um erro: {e.Message}";
            }

            return View();
        }

        [HttpPost]
        public IActionResult Edicao(ContatoEdicaoModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["MensagemAlerta"] = "Ocorreram erros no preechimento do formulário de edição, por favor, verifique";
                return View();
            }
            try
            {
                var contato = new Contato
                {
                    IdContato = model.IdContato,
                    Nome = model.Nome,
                    Email = model.Email,
                    Telefone = model.Telefone,
                    DataNascimento = DateTime.Parse(model.DataNascimento),
                    Tipo = int.Parse(model.Tipo)
                };

                var contatoRepository = new ContatoRepository();
                contatoRepository.Update(contato);


                TempData["MensagemSucesso"] = $"Contato {contato.Nome} atualizado com sucesso.";
                return RedirectToAction("Consulta");

            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Ocorreu um erro: {e.Message}";
            }
            
            return View();
        }

        public IActionResult Exclusao (Guid id)
        {
            try
            {
                var contatoRepository = new ContatoRepository();
                var contato = contatoRepository.GetById(id);
                var ususarioAutenticado = UsuarioAutenticadoService.GetUsuarioAutenticado(HttpContext);

                if (contato == null || contato.IdUsuario != ususarioAutenticado.IdUsuario)
                {
                    TempData["MensagemAltera"] = "Nenhum contato foi encontrado!";
                    return RedirectToAction("Consulta");
                }

                contatoRepository.Delete(contato);
                TempData["MensagemSucesso"] = $"Contato {contato.Nome} excluído com sucesso!";
                return RedirectToAction("Consulta");
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Ocorreu um erro: {e.Message}";
            }
            
            return RedirectToAction("Consulta");
        }

        public IActionResult Relatorio()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Relatorio(ContatoRelatorioModel model)
        {
            if(!ModelState.IsValid)
            {
                TempData["MensagemAlerta"] = "Ocorreram erros no preechimento das opções do Relatório, por favor, verifique";
                return View();
            }

            try
            {
                string tipoArquivo = null; //MIME TYPE
                string nomeArquivo = $"contatos_{DateTime.Now.ToString("ddMMyyyyHHmmss")}";
                IContatoReport contatosReport = null;

                switch(model.Formato)
                {
                    case "Pdf":
                        tipoArquivo = "application/pdf";
                        nomeArquivo += ".pdf";
                        contatosReport = new ContatoReportServicePdf();
                        break;
                    case "Excel":
                        tipoArquivo = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        nomeArquivo += ".xlsx";
                        contatosReport = new ContatoReportServiceExcel();
                        break;
                }
                var usuarioAutenticado = UsuarioAutenticadoService.GetUsuarioAutenticado(HttpContext);
                var usuario = new Usuario
                {
                    IdUsuario = usuarioAutenticado.IdUsuario,
                    Nome = usuarioAutenticado.Nome,
                    Email = usuarioAutenticado.Email
                };
                var contatoRepository = new ContatoRepository();

                var contatos = contatoRepository.GetAllByUsuario(usuarioAutenticado.IdUsuario);
                var arquivo = contatosReport.CreateReport(contatos, usuario);

                return File(arquivo, tipoArquivo, nomeArquivo);

            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Ocorreu um erro: {e.Message}";
            }        
            return View();
        }
    }
}
