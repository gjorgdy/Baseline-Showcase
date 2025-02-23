namespace Core.Interfaces;

public interface IMail
{

    bool SendMail(string from, string to, string subject, string body);

}