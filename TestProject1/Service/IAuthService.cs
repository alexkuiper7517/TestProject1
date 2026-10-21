using TestProject1.Models.DTOs;

namespace TestProject1.Services;

public interface IAuthService
{
    TokenResponse GenerateToken();
}