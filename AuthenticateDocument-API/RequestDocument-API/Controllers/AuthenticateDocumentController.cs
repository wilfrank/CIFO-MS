using Cifo.Model.Document;
using Cifo.Model.Util;
using Cifo.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CIFO.RequestDocument_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticateDocumentController : Controller
    {
        private readonly ILogger _logger;
        private readonly IGovFolderService _authenticationServices;

        public AuthenticateDocumentController(ILogger logger,
                                         IGovFolderService authenticationServices)
        {
            _authenticationServices = authenticationServices;
            _logger = logger;
        }


        [HttpPut("AuthenticateDocument")]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Task<AuthenticateDocumentCompleteModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> AuthenticateDocument(AuthenticateDocumentCompleteModel archivo)
        {
            try
            {
                return Ok(_authenticationServices.AuthenticationDocument(archivo));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty, null);
                return BadRequest(new ApiException(ex));
            }
        }
    }
}
