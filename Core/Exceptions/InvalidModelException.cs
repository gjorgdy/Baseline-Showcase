namespace Core.Exceptions;

public class InvalidModelException(List<string?> messages) : ArgumentException
{
    public List<string?> Messages => messages;

    public override string Message => string.Join("\n", messages);
}