using System.ComponentModel.DataAnnotations;

namespace Agenda.Presentation.Models
{
    /// <summary>
    /// Modelo de dados para capturar os campos do formulário de cadastro de usuário
    /// </summary>
    public class AccountRegisterModel
    {
        [RegularExpression("^[A-Za-zÀ-Üà-ü\\s]{6,150}$", ErrorMessage = "Por favor, informe um nome válido de 6 a 150 caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o seu nome.")]
        public string Nome { get; set; }
        
        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o seu email.")]
        public string Email { get; set; }
        
        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(20, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a sua senha.")]
        public string Senha { get; set; }
        
        [Compare("Senha", ErrorMessage = "Senhas não conferem, por favor verifique.")]
        [Required(ErrorMessage = "Por favor, confirme a sua senha.")]
        public string SenhaConfirmada { get; set; }
    }
}
