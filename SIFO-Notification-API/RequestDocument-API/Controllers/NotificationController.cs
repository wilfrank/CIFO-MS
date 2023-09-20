using Microsoft.AspNetCore.Mvc;
using SIFO.Controllers;
using SIFO.Models.Util;
using SIFO.Services.Messages;

namespace SIFO.RequestDocument_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotificationController : Controller
    {
        private readonly ILogger<SystemController> _logger; 
        private readonly INotificationService _notificationService;

        public NotificationController(
           ILogger<SystemController> logger,
           INotificationService notificationService
           )
        {
            _logger = logger;
            _notificationService = notificationService;
        }

        [HttpGet("SendNotification/{email}/{subject}/{emailText}")]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Task<List<int>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> SendNotification(string email,string subject, string emailText)
        {
            try
            {
                _notificationService.CreateAndSendEmailNotificationForUserAsync(emailText, email, subject);
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
