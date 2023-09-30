using RabbitMQ.Client;

namespace SubscriberWorker
{
    internal class Program
    {
        private static IConnectionFactory _factory;
        private static string _queueName = "transfer-user";
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            _factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5673
            };
            var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(_queueName, exclusive: false);
            var consumer = new MessageReceiver(channel);
            channel.BasicConsume(_queueName, false, consumer);
            Console.ReadLine();
        }
    }
}