using Cifo.Model;
using Microsoft.AspNetCore.Authorization;
using RabbitMQ.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MessagingWorker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransferCitizenController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private IConnectionFactory _factory;
        private string _queueName = "transfer-user";
        public TransferCitizenController(ILogger<WeatherForecastController> logger, IConnectionFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }

        [HttpPost()]
        [Route("transfer")]
        [AllowAnonymous]
        public async Task<IActionResult> Transfer(OperatorDto @operator)
        {
            var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(_queueName, exclusive: false);
            var jsonMsg = JsonConvert.SerializeObject(@operator);
            var body = Encoding.UTF8.GetBytes(jsonMsg);
            channel.BasicPublish(exchange: "", routingKey: _queueName, body: body);
            return Ok();
        }

        [HttpPost()]
        [AllowAnonymous]
        public async Task<IActionResult> RecieverCitizen(TransferDocDto transferUser)
        {
            _logger.LogInformation($"Recieving new user to be saved {transferUser.CitizenName}");
            return Ok(transferUser);
        }

    }
}
