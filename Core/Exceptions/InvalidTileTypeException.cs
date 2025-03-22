namespace Core.Exceptions;

public class InvalidTileTypeException(string type) : TileException($"Type '{type}' is not a valid tile type.")
{
    
}