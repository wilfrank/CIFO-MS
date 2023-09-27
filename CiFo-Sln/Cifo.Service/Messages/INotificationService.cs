using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifo.Service.Messages
{
    public interface INotificationService
    {
        Task CreateAndSendEmailNotificationForUserAsync(string emailText, string emailMember, string subject);
        Task EmailSenderSimple(IConfiguration configuration, EmailSenderSimpleModel emailSenderSimpleModel);
        Task CreateAndSendEmailNotificationMultiple(string bodyEmail, string subject, List<string> emails, List<string> emailsCC = null, List<string> emailsCCO = null);

        Dictionary<string, string> GetServiceParameters();
    }
}
