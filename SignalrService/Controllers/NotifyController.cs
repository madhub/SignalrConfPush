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
    public class NotifyController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> notificationHub;

        public NotifyController(IHubContext<NotificationHub> notificationHub)
        {
            this.notificationHub = notificationHub;
        }
        /// <summary>
        /// Send message to connected clients to with message provided in the payload
        /// </summary>
        /// <param name="messageData"> message payload</param>
        /// <returns></returns>
        [HttpPost]
        [Route("notify")]
        public async Task<ActionResult> Notify([FromBody] MessageData messageData)
        {
            LogHelper.Log($"Notifying all members of  group '{messageData.groupName}' with message '{messageData.message}'", ConsoleColor.Yellow);
            await notificationHub.Clients.Groups(messageData.groupName).SendAsync("ReceiveMessage",messageData.message);
            return Ok();
        }

        /// <summary>
        /// Send message to connected clients to changes log level 
        /// </summary>
        /// <param name="logData">LogData data model</param>
        /// <returns></returns>
        [HttpPost]
        [Route("changloglevel")]
        public async Task<ActionResult> Changloglevel([FromBody] LogData logData)
        {
            var logMsg = $"{logData.logLevel}|{logData.logCategory}";
            await notificationHub.Clients.Groups(logData.groupName).SendAsync("ChangeLogLevel", logMsg);
            return Ok();
        }

    }
}