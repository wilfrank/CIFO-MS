using Cifo.Model.Messages;
using Cifo.Model.Util;
using Cifo.Service.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace SIFO.RequestDocument_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : Controller
    {
        private readonly ILogger _logger; 
        private readonly INotificationService _notificationService;

        public NotificationController(
           ILogger<NotificationController> logger,
           INotificationService notificationService
           )
        {
            _logger = logger;
            _notificationService = notificationService;
        }

        [HttpPost]
        [Route("SendNotification")]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Task<List<int>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> SendNotification(EmailModel emailModel)
        {
            try
            {
                _notificationService.CreateAndSendEmailNotificationForUserAsync(emailModel.EmailText, emailModel.Email, emailModel.Subject);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty, null);
                return BadRequest(new ApiException(ex));
            }
        }
    }
}
