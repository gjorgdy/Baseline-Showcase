namespace WebServer.Models;

public class AuthPageModel
{

    private static AuthPageModel? _instance;

    private AuthPageModel() {}

    public List<AuthenticationPlatform> Platforms { get; set; } = [];

    public class AuthenticationPlatform(string platform, string url)
    {
        public string Platform { get; set; } = platform;
        public string Url { get; set; } = url;
    }

    public static AuthPageModel GetInstance()
    {
        _instance = new AuthPageModel();
        _instance.Platforms.Add(
            new AuthenticationPlatform(
                "Discord",
                "https://discord.com/oauth2/authorize?client_id=1115260593176330241&response_type=code&redirect_uri=https%3A%2F%2Flocalhost%3A7166%2Fauth%2Fsuccess&scope=identify+connections+guilds+openid"
            )
        );
        _instance.Platforms.Add(
            new AuthenticationPlatform(
                "Google",
                "https://accounts.google.com/o/oauth2/auth?scope=openid&response_type=code&access_type=offline&redirect_uri=https://localhost:44350/auth/callback/google&client_id=239855070043-9jmq31hrfh6gnhotvjvg9mlkeso8uv25.apps.googleusercontent.com"
            )
        );
        return _instance;
    }

}