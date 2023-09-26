using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIFO.Models.Util;
using SIFO.Services.Messages;
using System.Reflection;
using System.Security.Claims;

namespace SIFO.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SystemController:ControllerBase
    {
        private readonly ILogger<SystemController> _logger;
        private readonly INotificationService _notificationService;

        public SystemController(
            ILogger<SystemController> logger,
            INotificationService notificationService
            )
        {
            _logger = logger;
            _notificationService = notificationService;
        }

        [HttpGet("testEmail/{email}")]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> TestEmail(string email)
        {
            try
            {
                await _notificationService.CreateAndSendEmailNotificationForUserAsync("Este es un mensaje de prueba", email, " Mensaje de prueba");
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
