using eBoxOffice.Domain;
using eBoxOffice.Domain.Domain_models;
using eBoxOffice.Repository.Interface;
using eBoxOffice.Service.Interface;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace eBoxOffice.Service.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(EmailSettings settings)
        {
            _settings = settings;
        }


        public async Task SendEmailAsync(List<EmailMessage> allMails)
        {
            List<MimeMessage> messages = new List<MimeMessage>();
            foreach(var item in allMails)
            {
                var emailMessage = new MimeMessage
                {
                    Sender = new MailboxAddress(_settings.SendersName, _settings.SmtpUserName),
                    Subject = item.Subject
                };

                emailMessage.From.Add(new MailboxAddress(_settings.EmailDisplayName, _settings.SmtpUserName));
                emailMessage.To.Add(new MailboxAddress(item.MailTo, item.MailTo));

                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
                {
                    Text = item.Body
                };

                messages.Add(emailMessage);
            }

            try
            {
                using(var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    var socketOptions = _settings.EnableSSL ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto;

                    await smtp.ConnectAsync(_settings.SmtpServer, _settings.SmtpServerPort, socketOptions);

                    if (!string.IsNullOrEmpty(_settings.SmtpUserName))
                    {
                        await smtp.AuthenticateAsync(_settings.SmtpUserName, _settings.SmtpPassword);
                    }

                    foreach(var item in messages)
                    {
                        await smtp.SendAsync(item);
                    }

                    await smtp.DisconnectAsync(true);
                }
            }
            catch(SmtpException ex)
            {
                throw ex;
            }
        }
    }
}
