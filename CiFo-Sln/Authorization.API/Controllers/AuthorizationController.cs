using Cifo.Service.Interfaces;
using CiFo.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorizationController : ControllerBase
    {
        private readonly IFirebaseAuthService _firebaseAuth;
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        public AuthorizationController(ILogger<AuthorizationController> logger, IFirebaseAuthService firebaseAuth, IUserService userService)
        {
            _logger = logger;
            _firebaseAuth = firebaseAuth;
            _userService = userService;
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> Get(string key)
        {
            _logger.LogInformation($"Getting user info to {key}");
            var user = await _userService.GetById(key);
            return Ok(user);
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(SignUpModel user)
        {
            _logger.LogInformation($"Login user {user.Email}");
            var _userFb = await _firebaseAuth.Login(user);
            return Ok(_userFb);
        }
    }
}
