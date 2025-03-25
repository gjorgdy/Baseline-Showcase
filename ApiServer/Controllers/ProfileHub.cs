using Microsoft.AspNetCore.SignalR;

namespace ApiServer.Controllers;

public class ProfileHub : Hub
{

    public async Task JoinGroup(int userId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userId.ToString());
    }
    
}