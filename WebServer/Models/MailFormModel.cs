namespace Core.Models;

public class MailFormModel(int receiverId)
{

    public MailFormModel() : this(-1) { }

    public int ReceiverId { get; init; } = receiverId;

    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
}