using Core.Authentication;
using Core.Models;

namespace Tests.Authentication;

public class Tests
{
    private JwtTokenHandler _handler;
    
    [SetUp]
    public void Setup()
    {
        _handler = new JwtTokenHandler();
    }

    [Test]
    public void AsymmetryTest()
    {
        const int input = 204040550;
        string token = _handler.CreateToken(new UserData(input, "", "", [], []));
        int? output = JwtTokenHandler.GetUserId(_handler.ValidateToken(token));
        Assert.That(output, Is.EqualTo(input));
    }
}