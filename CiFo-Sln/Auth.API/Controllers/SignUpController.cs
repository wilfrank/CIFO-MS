using Cifo.Model;
using Cifo.Model.GovFolder;
using Cifo.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SignUpController : ControllerBase
    {
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
        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(UserModel user)
        {
            _logger.LogInformation($"Siging up user {user.Email}..");
            var validate = await _govFolderService.ValidateCitizen(user.IdentityNumber);
            if (!string.IsNullOrEmpty(validate))
            {
                _logger.LogInformation($"Siging up user Error {validate}..");
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
    }
}
