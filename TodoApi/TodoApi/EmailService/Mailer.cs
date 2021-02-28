
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TodoApi.EmailService
{

    public interface IMailer
    {
        Task SendEmailAsync(string email, string subject, string message);
    }

    public class Mailer : IMailer
    {
        private readonly IConfiguration _configuration;
        public Mailer(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using (var client = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = _configuration["SmtpSettings:SenderEmail"],
                    Password = _configuration["SmtpSettings:Password"]
                };

                client.Credentials = credential;
                client.Host = _configuration["SmtpSettings:Server"];
                client.Port = int.Parse(_configuration["SmtpSettings:Port"]);
                client.EnableSsl = true;

                using (var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress(email));
                    emailMessage.From = new MailAddress(_configuration["SmtpSettings:SenderEmail"]);
                    emailMessage.Subject = subject;
                    emailMessage.Body = message;
                    client.Send(emailMessage);
                }
            }
            await Task.CompletedTask;
        }
    }
    
}
