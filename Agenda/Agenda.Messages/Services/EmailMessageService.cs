using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Messages.Services
{
    /// <summary>
    /// Classe para realizar um serviço de envio de email
    /// </summary>
    public class EmailMessageService
    {
        private string _conta = "email@email.com.br";
        private string _senha = "1234235";
        private string _smtp = "smpt-mail.outlook.com";
        private int _porta = 587;

        public void SendMessage(string to, string subject, string body)
        {
            var mailMessage = new MailMessage(_conta, to);

            mailMessage.Subject = subject;
            mailMessage.Body = subject;
            mailMessage.IsBodyHtml = true;

            var smtpClient = new SmtpClient(_smtp, _porta);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(_conta, _senha);
            smtpClient.Send(mailMessage);
        }
    }
}
