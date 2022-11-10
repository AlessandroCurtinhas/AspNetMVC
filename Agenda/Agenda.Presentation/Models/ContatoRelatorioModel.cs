using System.ComponentModel.DataAnnotations;

namespace Agenda.Presentation.Models
{
    public class ContatoRelatorioModel
    {
        [Required(ErrorMessage = "Por favor, selecione o formato do relatório.")]
        public string Formato { get; set; }
    }
}
