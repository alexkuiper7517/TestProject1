using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TestProject1.Models.DTOs;

namespace TestProject1.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TokenResponse GenerateToken()
    {
        // Read JWT configuration
        var issuer = _configuration["Jwt:Issuer"]!;
        var audience = _configuration["Jwt:Audience"]!;
        var signingKey = _configuration["Jwt:SigningKey"]!;
        var expirationMinutes =
            _configuration.GetValue<int>("Jwt:ExpirationMinutes");

        // Create signing credentials
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(signingKey));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        // Define token claims
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, "demo-user"),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Create JWT
        var now = DateTime.UtcNow;

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            notBefore: now,
            expires: now.AddMinutes(expirationMinutes),
            signingCredentials: credentials
        );

        // Serialize and return
        var tokenString = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return new TokenResponse(
            tokenString,
            "Bearer",
            expirationMinutes * 60
        );
    }
}