using Agenda.Presentation.Models.Validators;
using System.ComponentModel.DataAnnotations;

namespace Agenda.Presentation.Models
{
    public class AccountLoginModel
    {
        [Required(ErrorMessage = "Por favor, informe seu email de acesso.")]
        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        public string Email { get; set; }

        [PasswordValidator(ErrorMessage = "Senha inválida.")]
        [Required(ErrorMessage = "Por favor, informe sua senha de acesso.")]
        public string Senha { get; set; }
    }
}
