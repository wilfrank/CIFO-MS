using RabbitMQ.Client;

namespace SubscriberWorker
{
    internal class Program
    {
        private static IConnectionFactory _factory;
        private static string _queueName = "transfer-user";
        private static HttpClient _httpClient;
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            _factory = new ConnectionFactory()
            {
                Uri = new Uri("amqps://omgrtclt:PCXsM2MLlHrn5C4TZZrVOlW7ZxbLMBel@turkey.rmq.cloudamqp.com/omgrtclt")
            };

            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:5226/api/")
            };
            var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(_queueName, exclusive: false);
            var consumer = new MessageReceiver(channel, _httpClient);
            channel.BasicConsume(_queueName, false, consumer);
            Console.WriteLine(" [*] Waiting for messages.");
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}