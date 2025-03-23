using Core.Platforms;
using Microsoft.Extensions.Caching.Memory;
using static NUnit.Framework.Assert;

namespace Tests.PlatformInteraction;

public class DiscordTest
{
    private DiscordApiHandler _api;
    
    [SetUp]
    public void Setup()
    {
        _api = new DiscordApiHandler(new HttpClient(), new MemoryCache(new MemoryCacheOptions()));
    }

    [Test]
    public void DisplayNameTest()
    {
        const string input = "422431842310946857";
        string? displayName = _api.GetDisplayName(input).Result;
        Console.Out.WriteLine(displayName);
        That(displayName, Is.Not.Null);
    }

    [Test]
    public void ProfilePictureTest()
    {
        const string input = "214323983586164736";
        string? profilePictureUri = _api.GetProfilePictureUri(input).Result;
        Console.Out.WriteLine(profilePictureUri);
        That(profilePictureUri, Is.Not.Null);
    }
}