using books_store_BLL.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace books_store_BLL.Dtos.Services
{
    public class EmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly SmtpSettings _smtpSettings;

        public EmailService( IOptions<SmtpSettings> options)
        {
            _smtpSettings = options.Value;

            _smtpClient = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port);
            _smtpClient.EnableSsl = true;
            _smtpClient.Credentials = new NetworkCredential(_smtpSettings.Email, _smtpSettings.Password);
        }
        public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = false)
        {
            var message = new MailMessage();
            message.From = new MailAddress(_smtpSettings.Email);
            message.To.Add(to);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = isHtml;

            await _smtpClient.SendMailAsync(message);
        }

    }
}
