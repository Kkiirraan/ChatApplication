using ChatApplication.AuthenticationService.Domain.Entities;

namespace ChatApplication.AuthenticationService.Application.Interfaces;

public interface ITokenService
{
    (string Token, DateTime ExpiresAtUtc) GenerateAccessToken(User user);
}
