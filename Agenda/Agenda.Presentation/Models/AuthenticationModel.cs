namespace Agenda.Presentation.Models
{
    /// <summary>
    /// Modelo de Dados para as informações do usuário autenticado
    /// que serão gravadas no COOKIE de autenticação do AspNet
    /// </summary>
    public class AuthenticationModel
    {
        public Guid IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataHoraAcesso { get; set; }

    }
}
