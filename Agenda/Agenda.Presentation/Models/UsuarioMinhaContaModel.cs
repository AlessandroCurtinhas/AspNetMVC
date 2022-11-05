using Agenda.Presentation.Models.Validators;
using System.ComponentModel.DataAnnotations;

namespace Agenda.Presentation.Models
{
    public class UsuarioMinhaContaModel
    {
        [PasswordValidator(ErrorMessage = "Informe de 8 a 20 caracteres com pelo menos 1 letra minúscula, 1 letra maiúscula, 1 número e um caractere especial.")]
        [Required(ErrorMessage = "Por favor, informe a sua nova senha.")]
        public string NovaSenha { get; set; }
        [Compare("NovaSenha", ErrorMessage = "Senhas não conferem, por favor verifique.")]
        [Required(ErrorMessage = "Por favor, confirme a sua senha.")]
        public string NovaSenhaConfirmacao { get; set; }
    }
}
