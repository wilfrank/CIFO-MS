using CIFO.Models.Models;
using CIFO.Models.Util;
using Microsoft.AspNetCore.Mvc;

namespace CIFO.RequestDocument_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GetDocumentController : Controller
    {
        private readonly ILogger<SystemController> _logger;

        public GetDocumentController(ILogger<SystemController> logger)
        {
            _logger = logger;
        }


        [HttpGet("GetDocumentsByUserId/{userId}")]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Task<List<DocumentModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetDocumentsByUserId(int userId)
        {
            try
            {
                List<DocumentModel> documents = new List<DocumentModel>();

                DocumentModel d = new DocumentModel { IdUser=1234567891, DocumentName="tarjeta profesional",Status="Autenticado"};
                documents.Add(d);
                return Ok(documents);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty, null);
                return BadRequest(new ApiException(ex));
            }
        }
    }
}
