using AuthMicroservice.Models;
using AuthMicroservice.Services.Interfaces;
using Firebase.Auth;
using Microsoft.AspNetCore.Mvc;

namespace AuthMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {

        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(ILogger<AuthController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost]
        [Route("SignIn")]
        public void SignUp(SignUpModel signData)
        {
            _logger.Log(LogLevel.Information, "Creating user data");
            _authService.SignUp(signData);
        }
        [HttpGet]
        [Route("LogIn/{email}/{password}")]
        public async Task<IActionResult> LogIn(string email, string password)
        {
            _logger.Log(LogLevel.Information, "Login user data");
            var validateLogIn = await _authService.Login(email, password);
            if (!string.IsNullOrEmpty(validateLogIn.Id))
            {
                return Ok(validateLogIn);
            }
            return Unauthorized(validateLogIn);
        }
    }
}
