using MessagingWorker.API.Messaging;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace MessagingWorker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private IConnectionFactory _factory;
        private string _queueName = "transfer-user";

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5673
            };
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var _values = Enumerable.Range(1, 2).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            });

            var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(_queueName, exclusive: false);
            var jsonMsg = JsonConvert.SerializeObject(_values);
            var body = Encoding.UTF8.GetBytes(jsonMsg);
            channel.BasicPublish(exchange: "", routingKey: _queueName, body: body);
            var consumer = new MessageReceiver(channel, _logger);
            channel.BasicConsume(_queueName, false, consumer);
            return Ok(_values.ToList());
        }
    }
}