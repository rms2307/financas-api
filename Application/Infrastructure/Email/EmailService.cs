using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Application.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly string _remetente;
        private readonly string _smtpHost;
        private readonly string _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;

        public EmailService(string remetente, string smtpHost, string smtpPort, string smtpUser, string smtpPass)
        {
            _remetente = remetente;
            _smtpHost = smtpHost;
            _smtpPort = smtpPort;
            _smtpUser = smtpUser;
            _smtpPass = smtpPass;
        }

        public async Task SendEmail(string emailAdress, string emailSubject, string emailBody)
        {
            try
            {
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(_remetente);
                mail.To.Add(emailAdress);
                mail.Subject = emailSubject;
                mail.Body = emailBody;
                mail.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient(_smtpHost, Convert.ToInt32(_smtpPort));
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
                smtpClient.EnableSsl = true;

                await smtpClient.SendMailAsync(mail);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao enviar e-mail: " + e.Message);
            }
        }
    }
}
