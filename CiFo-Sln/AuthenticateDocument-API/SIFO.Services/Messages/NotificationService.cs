using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIFO.Services.Messages
{
    public class NotificationService : INotificationService 
    {
        public async Task<bool> SendEmailConfirmation(string emailText, string emailMember, string subject)
        {
            try
            {
               // _messageProducer.SendingMessage<>();
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
    }
}
