using Microsoft.AspNetCore.SignalR;

namespace VL.Solar.SolarWorker.Hubs;

public class NotificationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var teams = GetTeamFromToken(); 
        foreach (var team in teams)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, team);
            await base.OnConnectedAsync();
        }
    }
    private List<string> GetTeamFromToken()
    {
        // Haal teams op van de token
        var token = Context.GetHttpContext().Request.Query["access_token"];
        List<string> teams = token.ToList();

        return teams;
    }
    public async Task StuurtBerichtNaarGroep(string groupName, string message)
    {
        await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
    }
    public async Task AddToGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        await Clients.Caller.SendAsync("GroupJoined", groupName);
    }
    public async Task RemoveFromGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        await Clients.Caller.SendAsync("GroupLeft", groupName);
    }
}
