using System.ComponentModel.DataAnnotations;
using Core.Validation;

namespace WebServer.Models;

public class MailFormModel(int receiverId = -1, bool sent = false) : ValidatedModel
{

    // ReSharper disable once UnusedMember.Global
    public MailFormModel() : this(-1) { }

    public int ReceiverId { get; init; } = receiverId;

    public bool Sent { get; } = sent;

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

    [Length(2, 32)]
    public string Subject { get; init; } = string.Empty;

    [Length(2, 1024)]
    public string Body { get; init; } = string.Empty;
}
