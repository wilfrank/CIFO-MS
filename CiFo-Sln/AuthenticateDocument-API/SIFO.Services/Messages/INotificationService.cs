using CIFO.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIFO.Services.Messages
{
    public interface INotificationService
    {
        Task<bool> SendEmailConfirmation(string emailText, string emailMember, string subject);
    }
}
