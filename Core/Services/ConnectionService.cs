using Core.Interfaces;

namespace Core.Services;

public class ConnectionService(IConnectionAccess connectionAccess) : IConnectionAccess
{
    public Task<bool> AddConnection(int userId, string platform, string platformId) 
        => connectionAccess.AddConnection(userId, platformId, platformId);

    public Task<bool> DeleteConnection(int userId, string platform)
        => connectionAccess.DeleteConnection(userId, platform);
}