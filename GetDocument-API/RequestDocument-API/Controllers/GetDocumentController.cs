using CIFO.Models.Models;
using CIFO.Models.Util;
using CIFO.Services.Document;
using Microsoft.AspNetCore.Mvc;

namespace CIFO.RequestDocument_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GetDocumentController : Controller
    {
        private readonly ILogger<SystemController> _logger;
        private readonly IDocumentService _documentService;


        public GetDocumentController(ILogger<SystemController> logger,
                                    IDocumentService documentService)
        {
            _logger = logger;
            _documentService = documentService;
        }


        [HttpGet("GetDocumentsByUserId/{userId}")]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Task<List<DocumentModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetDocumentsByUserId(int userId)
        {
            try
            {
                return Ok(await _documentService.GetDocumentsByUserId(userId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty, null);
                return BadRequest(new ApiException(ex));
            }
        }
    }
}
