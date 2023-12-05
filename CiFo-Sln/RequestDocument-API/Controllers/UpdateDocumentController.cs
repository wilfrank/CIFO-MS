//using CIFO.Models.Models;
//using CIFO.Models.Util;
//using CIFO.Services.CertifyDocument;
using Cifo.Model;
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
        [ProducesResponseType(typeof(Task<UserModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateDocument(FileDataDTO archivo)
        {
            try
            {
                var userKey= User.Claims.First(c => c.Type == "user_id").Value;
                return Ok( await _CertifyDocumentService.UpdateDocument(archivo,userKey));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty, null);
                return BadRequest(new ApiException(ex));
            }
        }
        [HttpDelete]
        [Route("DeleteDocument")]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Task<UserModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteDocument(DocumentDto document)
        {
            try
            {
                var userKey = User.Claims.First(c => c.Type == "user_id").Value;
                return Ok(await _CertifyDocumentService.DeleteDocument(document.Url, userKey));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty, null);
                return BadRequest(new ApiException(ex));
            }
        }
    }
}
