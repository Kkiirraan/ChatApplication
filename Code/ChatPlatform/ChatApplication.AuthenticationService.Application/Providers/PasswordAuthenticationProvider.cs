using ChatApplication.AuthenticationService.Application.Common;
using ChatApplication.AuthenticationService.Application.Interfaces;
using ChatApplication.AuthenticationService.Domain.Enums;

namespace ChatApplication.AuthenticationService.Application.Providers;

public class PasswordAuthenticationProvider : IAuthenticationProvider
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public AuthProviderType ProviderType => AuthProviderType.Password;
    public PasswordAuthenticationProvider(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }
    public async Task<AuthenticationResult> AuthenticateAsync(AuthenticationRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByUsernameOrEmailAsync(request.Identifier, cancellationToken);
        if (user is null)
            return AuthenticationResult.Fail("User not found.");

        var credential = await _userRepository.GetCredentialAsync(user.Id, AuthProviderType.Password, cancellationToken);
        if (credential?.SecretHash is null)
            return AuthenticationResult.Fail("Password not set for this user.");

        if (string.IsNullOrEmpty(request.Secret) || !_passwordHasher.Verify(request.Secret, credential.SecretHash))
            return AuthenticationResult.Fail("Invalid credentials.");

        return AuthenticationResult.Success(user);
    }
}
