using Cifo.Model;
using Cifo.Service.Interfaces;
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

            if (routingKey == "transfer-user")
            {
                var message = Encoding.UTF8.GetString(body.ToArray());
                var dataM = JsonConvert.DeserializeObject<CitizenTransDto>(message);
                
                var jsonData = JsonConvert.SerializeObject(dataM.TransferDocDto);
                var data = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient {
                    BaseAddress = new Uri(dataM.UrlOperatorToChange)
                };

                client.PostAsync("transferCitizen", data);
            }
            _channel.BasicAck(deliveryTag, false);
        }
    }
}
