using CIFO.Models.Models;
using CIFO.Models.Util;
using CIFO.Services.CertifyDocument;
using Microsoft.AspNetCore.Mvc;

namespace CIFO.RequestDocument_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CertifyDocumentController : Controller
    {
        private readonly ILogger<SystemController> _logger;
        private readonly ICertifyDocumentService _CertifyDocumentService;

        public CertifyDocumentController(ILogger<SystemController> logger,
                                         ICertifyDocumentService certifyDocument)
        {
            _CertifyDocumentService = certifyDocument;
            _logger = logger;
        }


        [HttpPost("CertifyDocument")]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Task<FileDataDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult> CertifyDocument(FileDataDTO archivo)
        {
            try
            {
                return Ok(_CertifyDocumentService.CertifyDocument(archivo));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty, null);
                return BadRequest(new ApiException(ex));
            }
        }
    }
}
