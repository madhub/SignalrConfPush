using Microsoft.AspNetCore.SignalR;
using Utils;

public class NotificationHub :  Hub
{


    /// <summary>
    /// Subscribe for receiving messages by joining group
    /// </summary>
    /// <param name="groupName"></param>
    /// <returns></returns>
    public Task JoinGroup(string groupName)
    {
        LogHelper.Log($"GroupJoin request from Client '{Context.ConnectionId}' ", ConsoleColor.Green);
        return Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }


    /// <summary>
    /// unsubscribe for  group
    /// </summary>
    /// <param name="groupName"></param>
    /// <returns></returns>
    public Task LeaveGroup(string groupName)
    {
        LogHelper.Log($"LeaveGroup request from Client '{Context.ConnectionId}' ", ConsoleColor.Green);
        return Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }
}
