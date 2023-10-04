using Cifo.Model;
using Microsoft.AspNetCore.Authorization;
using RabbitMQ.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Cifo.Service.Interfaces;
using Cifo.Model.GovFolder;
using System.Net.Http;

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
        private readonly IGovFolderService _govFolderService;
        private readonly IUserService _userService;

        public TransferCitizenController(ILogger<WeatherForecastController> logger,
                                        IConnectionFactory factory,
                                        IGovFolderService govFolderService,
                                        IUserService userService)
        {
            _logger = logger;
            _factory = factory;
            _govFolderService = govFolderService;
            _userService = userService;

        }

        [HttpPost()]
        [Route("transfer")]
        public async Task<IActionResult> Transfer(OperatorCompleteDto @operator)
        {
            try
            {
                var validate = await _govFolderService.UnregisterCitizen(@operator.Operator);

                if (validate)
                {
                    var userKey = User.Claims.First(c => c.Type == "user_id").Value;
                    var user = await _userService.GetById(userKey);

                    if (user != null)
                    {
                        user.IsActived = false;
                        await _userService.CreateAsync(user);

                        var documents = user.Documents
                            .Select(x => new KeyValuePair<string, string[]>(x.Name,new[] { x.Url } ))
                            .ToDictionary(x => x.Key, x => x.Value);

                        TransferDocDto transferDocDto = new TransferDocDto
                        {
                            Id = int.Parse(user.IdentityNumber),
                            CitizenName = user.UserName,
                            CitizenEmail = user.Email,
                            UrlDocuments = documents
                        };

                        CitizenTransDto citizenDto = new CitizenTransDto
                        {
                            TransferDocDto = transferDocDto,
                            UrlOperatorToChange = @operator.UrlOperatorToChange

                        };

                        var connection = _factory.CreateConnection();
                        using var channel = connection.CreateModel();
                        channel.QueueDeclare(_queueName, exclusive: false);
                        var jsonMsg = JsonConvert.SerializeObject(citizenDto);
                        var body = Encoding.UTF8.GetBytes(jsonMsg);
                        channel.BasicPublish(exchange: "", routingKey: _queueName, body: body);
                    }


                }

                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
           
        }

    }
}
