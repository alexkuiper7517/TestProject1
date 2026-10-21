namespace TestProject1.Models.DTOs;

public record TokenResponse(
    string AccessToken,
    string TokenType,
    int ExpiresIn
);