using System.ComponentModel.DataAnnotations;

namespace Agenda.Presentation.Models
{
    public class ContatoCadastroModel
    {
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres ")]
        [MinLength(6, ErrorMessage = "Por favor, informe o mínimo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome do contato.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Por favor, informe o nome do contato.")]
        public string Telefone { get; set; }
        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o email do contato.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Por favor, informe a data de nascimento do contato.")]
        public string DataNascimento { get; set; }
        [Required(ErrorMessage = "Por favor, informe selectione o tipo de contato.")]
        public string Tipo { get; set; }


    }
}
