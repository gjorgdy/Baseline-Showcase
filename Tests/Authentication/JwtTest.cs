using Core.Authentication;

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
        const string input = "ThisIsATestString";
        string token = _handler.CreateToken(input);
        string output = _handler.ValidateToken(token);
        Assert.That(output, Is.EqualTo(input));
    }
}