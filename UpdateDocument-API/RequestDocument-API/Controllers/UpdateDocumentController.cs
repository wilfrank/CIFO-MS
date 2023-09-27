//using CIFO.Models.Models;
//using CIFO.Models.Util;
//using CIFO.Services.CertifyDocument;
using Cifo.Model.Document;
using Cifo.Model.Util;
using Cifo.Service.Document;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CIFO.RequestDocument_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UpdateDocumentController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUpdateDocumentService _CertifyDocumentService;

        public UpdateDocumentController(ILogger<UpdateDocumentController> logger,
                                         IUpdateDocumentService certifyDocument)
        {
            _CertifyDocumentService = certifyDocument;
            _logger = logger;
        }


        [HttpPost]
        [Route("UpdateDocument")]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Task<FileDataDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateDocument(FileDataDTO archivo)
        {
            try
            {
                return Ok( await _CertifyDocumentService.UpdateDocument(archivo));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty, null);
                return BadRequest(new ApiException(ex));
            }
        }
    }
}
