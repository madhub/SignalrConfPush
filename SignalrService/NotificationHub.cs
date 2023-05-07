using Microsoft.AspNetCore.SignalR;
using Utils;

public class NotificationHub :  Hub
{

    //// this will invoked from the API service to send message to group
    //internal async Task SendMessageToGroup(string groupName,string message)
    //{
    //    await Clients.Group(groupName).SendAsync(message);
    //}
    // client subscribe for  group
    public Task JoinGroup(string groupName)
    {
        LogHelper.Log($"GroupJoin request from Client '{Context.ConnectionId}' ", ConsoleColor.Green);
        return Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

    // client unsubscribe for  group
    public Task LeaveGroup(string groupName)
    {
        LogHelper.Log($"LeaveGroup request from Client '{Context.ConnectionId}' ", ConsoleColor.Green);
        return Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }
}
