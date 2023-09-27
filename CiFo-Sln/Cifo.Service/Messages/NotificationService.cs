using Microsoft.Azure.Documents.SystemFunctions;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifo.Service.Messages
{
    public class NotificationService : INotificationService
    {
        private readonly IConfiguration _configuration;

        public NotificationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task CreateAndSendEmailNotificationForUserAsync(string emailText, string emailMember, string subject)
        {
            var emailSenderSimpleModel = new EmailSenderSimpleModel
            {
                Subject = subject,
                ToEmail = emailMember,
                EmailText = emailText
            };

            await EmailSenderSimple(_configuration, emailSenderSimpleModel);
        }
        public async Task CreateAndSendEmailNotificationMultiple(string bodyEmail, string subject,
            List<string> emails, List<string> emailsCC = null, List<string> emailsCCO = null)
        {
            var emailSenderSimpleModel = new EmailSenderSimpleModel
            {
                Subject = subject,
                EmailText = bodyEmail
            };

            await EmailSenderMultiple(_configuration, emailSenderSimpleModel, emails, emailsCC, emailsCCO);
        }
        public async Task EmailSenderMultiple(IConfiguration configuration, EmailSenderSimpleModel emailSenderSimpleModel,
            List<string> emails, List<string> emailsCC = null, List<string> emailsCCO = null)
        {   //Envíar un correo a varias personas        

            var body = new TextPart("html")
            {
                Text = emailSenderSimpleModel.EmailText
            };
            var multipart = new Multipart { body };
            var emailSender = new EmailSender(configuration);
            emailSender.CreateMultipleEmails(emails, multipart, emailSenderSimpleModel.Subject, emailsCC, emailsCCO);
            await emailSender.SendMessageAsync();

        }
        public async Task EmailSenderSimple(IConfiguration configuration, EmailSenderSimpleModel emailSenderSimpleModel)
        {
            var body = new TextPart("html")
            {
                Text = emailSenderSimpleModel.EmailText
            };

            var multipart = new Multipart { body };

            var emailSender = new EmailSender(configuration);
            emailSender.CreateSimpleMessage(emailSenderSimpleModel.ToEmail, multipart, emailSenderSimpleModel.Subject);

            await emailSender.SendMessageAsync();
        }

        public Dictionary<string, string> GetServiceParameters()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("Email:From", _configuration["Email:From"]);
            parameters.Add("Email:Host", _configuration["Email:Host"]);
            parameters.Add("Email:Port", _configuration["Email:Port"]);
            parameters.Add("Email:EnableAuthetication", _configuration["Email:EnableAuthetication"]);
            parameters.Add("Email:Login", _configuration["Email:Login"]);

            return parameters;
        }

    }
}
