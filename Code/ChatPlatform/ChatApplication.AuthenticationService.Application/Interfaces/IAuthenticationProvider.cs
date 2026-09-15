using ChatApplication.AuthenticationService.Application.Common;
using ChatApplication.AuthenticationService.Domain.Enums;

namespace ChatApplication.AuthenticationService.Application.Interfaces;

public interface IAuthenticationProvider
{
    AuthProviderType ProviderType { get; }
    Task<AuthenticationResult> AuthenticateAsync(AuthenticationRequest request, CancellationToken cancellationToken = default);
}
