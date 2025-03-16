namespace Core.Interfaces;

public interface IDataAccess
{
    public AUserAccess? GetUserAccess(int userId);
    public AUserAccess? GetUserAccess(string platform, string platformId);
    public IRoleAccess GetRoleAccess();
}