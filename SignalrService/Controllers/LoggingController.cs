using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalrService.Model;
using Utils;

namespace SignalrService.Controllers
{
    /// <summary>
    /// Rest API for notifying connected clients & sending command to change the log level
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class LoggingController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> notificationHub;

        public LoggingController(IHubContext<NotificationHub> notificationHub)
        {
            this.notificationHub = notificationHub;
        }
        /// <summary>
        /// Send message to connected clients to changes log level 
        /// </summary>
        /// <param name="logData"> Log data model </param>
        /// <returns></returns>
        [HttpPost]
        [Route("changelevel")]
        public async Task<ActionResult> Changelevel([FromBody] LogData logData)
        {
            var logMsg = $"{logData.logLevel}|{logData.logCategory}";
            await notificationHub.Clients.Groups(logData.groupName).SendAsync("ChangeLogLevel", logMsg);
            return Ok();
        }

    }
}
