using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestProject1.Models.DTOs;
using TestProject1.Services;

namespace TestProject1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("token")]
    [AllowAnonymous]
    public ActionResult<TokenResponse> GenerateToken()
    {
        return Ok(_authService.GenerateToken());
    }
}