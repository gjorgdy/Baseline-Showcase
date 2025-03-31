using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Core.Models;
using Microsoft.IdentityModel.Tokens;

namespace Core.Authentication;

public class JwtTokenHandler
{
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly TokenValidationParameters _tokenValidationParameters;

    private readonly RsaSecurityKey _privateKey;

    private const string Audience = "https://api.baseline.nl/";
    private const string Issuer = "https://baseline.nl/";

    public JwtTokenHandler()
    {
        var publicKey = ReadKeyFromPem("keys/public_key.pem")!;
        _privateKey = ReadKeyFromPem("keys/private_key.pem");
        _tokenHandler = new JwtSecurityTokenHandler();
        _tokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = publicKey,
            ValidIssuer = Issuer,
            ValidAudience = Audience
        };
    }
    
    private static RsaSecurityKey ReadKeyFromPem(string pemFilePath)
    {
        string pemContent = File.ReadAllText(pemFilePath);
        var rsa = new RSACryptoServiceProvider();
        rsa.ImportFromPem(pemContent);
        return new RsaSecurityKey(rsa);
    }
    
    public string CreateToken(UserData userData)
    {
        var claims = new List<Claim> { new("user_id", userData.Id.ToString()) };
        claims.AddRange(
            userData.Roles
                .Select(role => new Claim(ClaimTypes.Role, role.Id))
        );
        SecurityToken token = _tokenHandler.CreateJwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            subject: new ClaimsIdentity(claims),
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: new SigningCredentials(
                _privateKey,
                SecurityAlgorithms.RsaSha256
            )
        );
        return _tokenHandler.WriteToken(token);
    }

    public ClaimsPrincipal ValidateToken(string token)
    {
        return _tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
    }

    public static bool HasAdminRole(ClaimsPrincipal principal)
    {
        try
        {
            return principal.Claims
                .Where(claim => claim.Type == ClaimTypes.Role)
                .Any(role => role.Value == "admin");
        }
        catch (Exception e)
        {
            return false;
        }
    }
    
    public static int? GetUserId(ClaimsPrincipal principal)
    {
        try
        {
            string idString = principal.Claims.First(c => c.Type == "user_id").Value;
            return int.Parse(idString);
        }
        catch (Exception e)
        {
            return null;
        }
    }
    
}