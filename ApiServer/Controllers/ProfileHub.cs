using Microsoft.AspNetCore.SignalR;

namespace ApiServer.Controllers;

public class ProfileHub : Hub
{

    public async Task JoinGroup(int userId)
    {
        try
        {
            await Console.Out.WriteLineAsync($"Attempting to join group {Context.ConnectionId} for user {userId}");

            // Attempt to add to group
            await Groups.AddToGroupAsync(Context.ConnectionId, userId.ToString());

            await Console.Out.WriteLineAsync($"Successfully joined group {Context.ConnectionId} for user {userId}");
        }
        catch (Exception ex)
        {
            // Log the exception
            await Console.Out.WriteLineAsync($"Error in JoinGroup: {ex.Message}");
            throw; // Rethrow exception to propagate the error
        }
    }
    
}