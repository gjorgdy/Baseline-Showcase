using System.Net;
using System.Net.Mail;
using Core.Interfaces;
using dotenv.net;

namespace MailTrap;

public class Mail : IMail
{
    private SmtpClient Client { get; init; }

    public Mail()
    {
        DotEnv.Load();
        var dotenv = DotEnv.Read();
        Client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
        {
            Credentials = new NetworkCredential(dotenv["MAILTRAP_USERNAME"], dotenv["MAILTRAP_PASSWORD"]),
            EnableSsl = true
        };
        Console.Out.WriteLine("Connected to SMTP server");
    }

    public bool SendMail(string from, string to, string subject, string body)
    {
        try
        {
            Client.Send(from, to, subject, body);
            Console.Out.WriteLine("Mail sent.");
            return true;
        }
        catch
        {
            return false;
        }
    }
}