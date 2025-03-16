namespace Core.Interfaces;

public abstract class AConnectionAccess
{
    public abstract Task<bool> AddConnection(string platform, string platformId);
    public abstract Task<bool> DeleteConnection(string platform);
}