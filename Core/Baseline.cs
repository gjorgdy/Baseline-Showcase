using Core.Interfaces;

namespace Core;

public class Baseline
{

    public static IMail MailService { get; private set; } = null!;

    public Baseline(IMail mailService)
    {
        MailService = mailService;
    }

}