using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace TestProject1.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Read JWT configuration
        var issuer = configuration["Jwt:Issuer"]!;
        var audience = configuration["Jwt:Audience"]!;
        var signingKey = configuration["Jwt:SigningKey"]!;

        // Configure JWT authentication
        services.AddAuthentication(
                JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.MapInboundClaims = false;

                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = issuer,

                        ValidateAudience = true,
                        ValidAudience = audience,

                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        ClockSkew = TimeSpan.Zero,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(signingKey))
                    };
            });

        // Register authorization services
        services.AddAuthorization();

        return services;
    }
}