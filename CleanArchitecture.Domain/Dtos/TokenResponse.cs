namespace CleanArchitecture.Domain.Dtos;

public sealed record TokenResponse(
    string Token,
    DateTime TokenExpiration,
    string RefreshToken,
    DateTime RefreshTokenExpiration
    );
