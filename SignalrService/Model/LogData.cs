namespace SignalrService.Model
{
    /// <summary>
    /// LogData data model
    /// </summary>
    /// <param name="groupName">Message group</param>
    /// <param name="logLevel"> Allowed values are , Information,Warning,Trace,Debug,Error,Critical</param>
    /// <param name="logCategory">Log category.Ex: SignalRClient.Worker</param>
    public record LogData(string groupName, string logLevel, string logCategory);
}