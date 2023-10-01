using Cifo.Model;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace SubscriberWorker
{
    internal class MessageReceiver : DefaultBasicConsumer
    {
        private readonly IModel _channel;
        private readonly HttpClient _httpClient;
        public MessageReceiver(IModel channel, HttpClient httpClient)
        {
            _channel = channel;
            _httpClient = httpClient;
        }
        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            Console.WriteLine($"Consuming Message");
            Console.WriteLine(string.Concat("Message received from the exchange ", exchange));
            Console.WriteLine(string.Concat("Consumer tag: ", consumerTag));
            Console.WriteLine(string.Concat("Delivery tag: ", deliveryTag));
            Console.WriteLine(string.Concat("Routing tag: ", routingKey));
            Console.WriteLine(string.Concat("Message: ", Encoding.UTF8.GetString(body.ToArray())));
            var userToTransfer = new TransferDocDto
            {
                Id = 1472589655,
                CitizenEmail = "maira23@cifo.com",
                CitizenName = "maira 23 Acevedo",
                UrlDocuments = new List<string>()
                {
                    {"https://firebasestorage.googleapis.com/v0/b/eafit-cifo.appspot.com/o/1256358953%2FDocumento7.PDF_9d6171f5-d1fc-44c0-8f37-0c05e799c9b2?alt=media&token=16f945b6-8ce7-4acc-89c3-2c5965e5283a"},
                    {"https://firebasestorage.googleapis.com/v0/b/eafit-cifo.appspot.com/o/1256358953%2FDocumento3.PDF_968f0165-26da-4817-a25c-27f90ca3122a?alt=media&token=77a81ffe-f0a2-4bf3-9dd0-27091d9df22c" }
                }
            };
            var json = JsonConvert.SerializeObject(userToTransfer);
            var bodyToSend = new StringContent(json, Encoding.UTF8, "application/json");
            var jsonData = JsonConvert.SerializeObject(bodyToSend);
            var data = new StringContent(jsonData, Encoding.UTF8, "application/json");
            
            // Get BaseUrl of Operator who goes to transfer
            
            _httpClient.PostAsync("TransferCitizen", data);
            _channel.BasicAck(deliveryTag, false);
        }
    }
}
