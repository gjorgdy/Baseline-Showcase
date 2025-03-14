namespace Core.Interfaces;

public interface IDataAccess
{

    public int GetUserId(string platform, string platformId);
    
    public string? GetUserDisplayName(int id);

}