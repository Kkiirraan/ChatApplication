
namespace ChatApplication.AuthenticationService.Application.DTOs;

public record AuthResponseDto(string AccessToken, DateTime ExpiresAtUtc, UserDto User);

