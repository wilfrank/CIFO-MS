using CIFO.Models.Models;
using CIFO.Models.Util;
using CIFO.Services.CertifyDocument;
using Microsoft.AspNetCore.Mvc;

namespace CIFO.RequestDocument_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UpdateDocumentController : Controller
    {
        private readonly ILogger<SystemController> _logger;
        private readonly IUpdateDocumentService _CertifyDocumentService;

        public UpdateDocumentController(ILogger<SystemController> logger,
                                         IUpdateDocumentService certifyDocument)
        {
            _CertifyDocumentService = certifyDocument;
            _logger = logger;
        }


        [HttpPost("UpdateDocument")]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Task<FileDataDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateDocument(FileDataDTO archivo)
        {
            try
            {
                return Ok(_CertifyDocumentService.UpdateDocument(archivo));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty, null);
                return BadRequest(new ApiException(ex));
            }
        }
    }
}
