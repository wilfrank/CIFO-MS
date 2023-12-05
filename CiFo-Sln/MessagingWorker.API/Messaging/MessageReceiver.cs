using RabbitMQ.Client;
using System.Text;

namespace MessagingWorker.API.Messaging
{
    public class MessageReceiver : DefaultBasicConsumer
    {
        private readonly IModel _channel;
        private readonly ILogger _logger;
        public MessageReceiver(IModel channel, ILogger logger)
        {
            _channel = channel;
            _logger = logger;
        }
        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            _logger.LogInformation("Consuming Message");
            _logger.LogInformation(string.Concat("Message received from the exchange ", exchange));
            _logger.LogInformation(string.Concat("Message: ", Encoding.UTF8.GetString(body.ToArray())));
            base.HandleBasicDeliver(consumerTag, deliveryTag, redelivered, exchange, routingKey, properties, body);
            //_channel.BasicAck(deliveryTag, false);
        }
        //public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, byte[] body)
        //{
        //    Console.WriteLine($"Consuming Message");
        //    Console.WriteLine(string.Concat("Message received from the exchange ", exchange));
        //    Console.WriteLine(string.Concat("Consumer tag: ", consumerTag));
        //    Console.WriteLine(string.Concat("Delivery tag: ", deliveryTag));
        //    Console.WriteLine(string.Concat("Routing tag: ", routingKey));
        //    Console.WriteLine(string.Concat("Message: ", Encoding.UTF8.GetString(body)));
        //    _channel.BasicAck(deliveryTag, false);
        //}
    }
}
