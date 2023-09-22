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
        private readonly IMessageProducer _messageProducer;
        public NotificationService(IMessageProducer messageProducer) 
        {
            _messageProducer = messageProducer;
        }

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
