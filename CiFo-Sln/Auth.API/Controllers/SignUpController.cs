using Cifo.Model;
using Cifo.Model.GovFolder;
using Cifo.Service.Interfaces;
using CiFo.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<SignUpController> _logger;
        private readonly IFirebaseAuthService _firebaseAuth;
        private readonly IGovFolderService _govFolderService;
        private readonly IUserService _userService;

        public SignUpController(ILogger<SignUpController> logger, IFirebaseAuthService firebaseAuthService
            , IGovFolderService govFolderService, IUserService userService)
        {
            _logger = logger;
            _firebaseAuth = firebaseAuthService;
            _govFolderService = govFolderService;
            _userService = userService;
        }

        [HttpGet]
        [Route("GetByKey")]
        [Authorize]
        public async Task<IActionResult> Get(string key)
        {
            var user = await _userService.GetById(key);
            return Ok(user);
        }
        [HttpPost]
        [Route("SignUp")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(UserModel user)
        {
            var validate = await _govFolderService.ValidateCitizen(user.IdentityNumber);
            if (!string.IsNullOrEmpty(validate))
            {
                return BadRequest(validate);
            }
            var citizen = new CitizenDto
            {
                address = user.Address,
                email = user.Email,
                name = $"{user.FirstName} {user.LastName}",
                id = int.Parse(user.IdentityNumber)
            };
            await _govFolderService.RegisterCitizen(citizen);
            var userData = await _firebaseAuth.SignUp(user);
            if (userData != null)
            {
                user.Documents = new List<DocumentDto>();
                user.Id = userData.LocalId;
                await _userService.CreateAsync(user);
            }
            return Ok(userData);
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(SignUpModel user)
        {
            var _userFb = await _firebaseAuth.Login(user);
            return Ok(_userFb);
        }
    }
}
