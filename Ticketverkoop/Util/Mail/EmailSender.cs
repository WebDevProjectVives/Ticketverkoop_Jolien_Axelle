using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Ticketverkoop.Util.Mail
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;
        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task SendEmailAsync(
            string email, string subject, string message)
        {
            var mail = new MailMessage();  // aanmaken van een mail‐object
            mail.To.Add(new MailAddress("ticketverkoop.avt.js@gmail.com"));
            mail.From = new
                    MailAddress("ticketverkoop.avt.js@gmail.com");
            mail.Subject = subject;
            mail.Body = email + "\n" + message;
            mail.IsBodyHtml = true;
            try
            {
                using (var smtp = new SmtpClient(_emailSettings.MailServer))
                {
                    smtp.Port = _emailSettings.MailPort;
                    smtp.EnableSsl = true;
                    smtp.Credentials =
                        new NetworkCredential(_emailSettings.Sender,
                                                _emailSettings.Password);
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
