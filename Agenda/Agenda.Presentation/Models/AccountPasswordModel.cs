using System.ComponentModel.DataAnnotations;

namespace Agenda.Presentation.Models
{
    /// <summary>
    /// Modelo de Dados para a página de recuperação de senha do usuário
    /// </summary>
    public class AccountPasswordModel
    {
        [EmailAddress (ErrorMessage = "Por favor, informe um endereço de email válido")]
        [Required]
        public string Email { get; set; }
    }
}
