using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Net;


namespace Cifo.Service.Messages
{
    public class EmailSender
    {
        private MimeMessage _message;

        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void CreateMultipleEmails(List<string> EmailsTo, MimeEntity messageBody,
            string subject, List<string> emailsCC = null, List<string> emailsCCO = null)
        {
            _message = new MimeMessage();
            _message.Body = messageBody;
            _message.Subject = subject;
            _message.From.Add(new MailboxAddress(_configuration["Email:From"], _configuration["Email:From"]));
            if (EmailsTo != null)
            {
                List<MailboxAddress> To = new List<MailboxAddress>();
                foreach (var email in EmailsTo)
                {
                    To.Add(new MailboxAddress(email, email));
                }
                _message.To.AddRange(To);
            }
            if (emailsCC != null)
            {
                foreach (var email in emailsCC)
                {
                    _message.Cc.Add(new MailboxAddress(email, email));
                }
            }

            if (emailsCCO != null)
            {
                foreach (var email in emailsCCO)
                {
                    _message.Bcc.Add(new MailboxAddress(email, email));
                }
            }
        }
        public void CreateSimpleMessage(string emailTo, MimeEntity messageBody, string subject, string[] CcEmails = null, string[] BccEmails = null)
        {
            _message = new MimeMessage
            {
                Subject = subject,
                Body = messageBody
            };

            _message.From.Add(new MailboxAddress(_configuration["Email:From"], _configuration["Email:From"]));
            _message.To.Add(new MailboxAddress(emailTo, emailTo));

            if (CcEmails != null)
            {
                foreach (var email in CcEmails)
                {
                    _message.Cc.Add(new MailboxAddress(email, email));
                }
            }

            if (BccEmails != null)
            {
                foreach (var email in BccEmails)
                {
                    _message.Bcc.Add(new MailboxAddress(email, email));
                }
            }
        }

        public async Task SendMessageAsync()
        {
            using (var client = new SmtpClient())
            {
                client.Connect(_configuration["Email:Host"], int.Parse(_configuration["Email:Port"]), SecureSocketOptions.Auto);

                bool.TryParse(_configuration["Email:EnableAuthetication"] ?? "false", out bool enableAuthetication);

                if (enableAuthetication)
                {
                    client.Authenticate(new NetworkCredential(_configuration["Email:Login"], _configuration["Email:Password"]));
                }

                await client.SendAsync(_message);
                client.Disconnect(true);
            }
        }
    }
}
