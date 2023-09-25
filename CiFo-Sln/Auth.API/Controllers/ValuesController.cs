using Cifo.Model;
using Cifo.Model.GovFolder;
using Cifo.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<ValuesController> _logger;
        private readonly IFirebaseAuthService _firebaseAuth;
        private readonly IGovFolderService _govFolderService;

        public ValuesController(ILogger<ValuesController> logger, IFirebaseAuthService firebaseAuthService, IGovFolderService govFolderService)
        {
            _logger = logger;
            _firebaseAuth = firebaseAuthService;
            _govFolderService = govFolderService;
        }

        [HttpGet]
        [Route("GetValues")]
        [Authorize]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp(UserModel user)
        {
            var validate = await _govFolderService.ValidateCitizen(user.IdentityNumber);
            if (!string.IsNullOrEmpty(validate))
            {
                return BadRequest();
            }
            var citizen = new CitizenDto
            {
                address = user.Address,
                email = user.Email,
                name = $"{user.FirstName} {user.LastName}",
                id = int.Parse(user.IdentityNumber)
            };
            var userGovFolder = await _govFolderService.RegisterCitizen(citizen);
            var userData = await _firebaseAuth.SignUp(user);
            return Ok(userData);
        }
    }
}
