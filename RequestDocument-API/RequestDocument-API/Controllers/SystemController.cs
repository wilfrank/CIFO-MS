using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CIFO.Models.Util;
using CIFO.Services.Messages;
using System.Reflection;
using System.Security.Claims;

namespace CIFO.RequestDocument_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SystemController
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


    }
}
