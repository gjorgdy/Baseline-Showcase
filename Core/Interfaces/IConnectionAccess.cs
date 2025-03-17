namespace Core.Interfaces;

public interface IConnectionAccess
{
    public Task<bool> AddConnection(int userId, string platform, string platformId);
    public Task<bool> DeleteConnection(int userId, string platform);
}