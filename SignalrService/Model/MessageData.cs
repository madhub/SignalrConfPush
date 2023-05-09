namespace SignalrService.Model
{
    /// <summary>
    /// Message data model
    /// </summary>
    /// <param name="groupName"> Message group</param>
    /// <param name="message"> Message </param>
    public record MessageData(string groupName, string message);
}