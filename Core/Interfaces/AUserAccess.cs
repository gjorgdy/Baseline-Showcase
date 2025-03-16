namespace Core.Interfaces;

public abstract class AUserAccess
{
    public abstract int GetId();
    public abstract string GetDisplayName();
    public abstract string GetProfilePicture();
    public abstract bool SetDisplayName(string displayName);
    public abstract bool SetProfilePicture(string picture);
    public abstract AConnectionAccess GetConnectionAccess();
    public abstract ATileAccess GetTileAccess();
}