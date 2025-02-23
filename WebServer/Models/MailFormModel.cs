using System.ComponentModel.DataAnnotations;
using Core.Validation;

namespace WebServer.Models;

public class MailFormModel(int receiverId) : ValidatedModel
{

    public MailFormModel() : this(-1) { }

    public int ReceiverId { get; init; } = receiverId;

    [Length(2, 16)]
    public string FirstName { get; init; } = string.Empty;

    [Length(2, 16)]
    public string LastName { get; init; } = string.Empty;

    [EmailAddress]
    [MinLength(6)]
    public string Email { get; init; } = string.Empty;

    [Phone]
    [Length(2, 12)]
    public string PhoneNumber { get; init; } = string.Empty;

    [Length(2, 1024)]
    public string Message { get; init; } = string.Empty;
}