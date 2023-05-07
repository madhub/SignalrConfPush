using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Utils;

namespace SignalrService.Controllers
{
    public record MessageData(string groupName, string message);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="groupName"></param>
    /// <param name="logLevel"> Allowed values are , Information,Warning,Trace,Debug,Error,Critical</param>
    /// <param name="logCategory">Log category.Ex: SignalRClient.Worker</param>
    public record LogData(string groupName, string logLevel,string logCategory);


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
        [HttpPost]
        [Route("notify")]
        public async Task<ActionResult> Notify([FromBody] MessageData payload)
        {
            LogHelper.Log($"Notifying all members of  group '{payload.groupName}' with message '{payload.message}'", ConsoleColor.Yellow);
            await notificationHub.Clients.Groups(payload.groupName).SendAsync("ReceiveMessage",payload.message);
            return Ok();
        }

        [HttpPost]
        [Route("changingloglevel")]
        public async Task<ActionResult> Changingloglevel([FromBody] LogData payload)
        {
            //LogHelper.Log($"Notifying all members of  group '{payload.groupName}' with message '{payload.message}'", ConsoleColor.Yellow);
            var logMsg = $"{payload.logLevel}|{payload.logCategory}";
            await notificationHub.Clients.Groups(payload.groupName).SendAsync("ChangeLogLevel", logMsg);
            return Ok();
        }

    }
}