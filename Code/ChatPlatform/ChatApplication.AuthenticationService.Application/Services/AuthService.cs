using ChatApplication.AuthenticationService.Application.Common;
using ChatApplication.AuthenticationService.Application.DTOs;
using ChatApplication.AuthenticationService.Application.Exceptions;
using ChatApplication.AuthenticationService.Application.Interfaces;
using ChatApplication.AuthenticationService.Domain.Entities;
using ChatApplication.AuthenticationService.Domain.Enums;

namespace ChatApplication.AuthenticationService.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IEnumerable<IAuthenticationProvider> _providers;
    private readonly ITokenService _tokenService;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenService tokenService,
        IEnumerable<IAuthenticationProvider> providers)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _providers = providers;
    }
    public async Task<UserDto> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        if (await _userRepository.ExistsAsync(request.Username, request.Email, cancellationToken))
        {
            throw new AppException("A user with the same username or email already exists.");
        }

        var user = new User
        {
            Username = request.Username.Trim(),
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Email = request.Email.Trim().ToLowerInvariant(),
            PhoneCountryCode = request.PhoneCountryCode.Trim(),
            PhoneNumber = request.PhoneNumber.Trim(),
            Birthday = request.Birthday.Value,
            Status = UserStatus.PendingVerification
        };

        await _userRepository.AddAsync(user, cancellationToken);

        var credential = new UserCredential
        {
            UserId = user.Id,
            Provider = AuthProviderType.Password,
            Identifier = user.Username,
            SecretHash = _passwordHasher.Hash(request.Password)
        };

        await _userRepository.AddCredentialAsync(credential, cancellationToken);

        await _userRepository.SaveChangesAsync(cancellationToken);

        return ToDto(user);
    }
    public async Task<AuthResponseDto> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var provider = _providers.FirstOrDefault(p => p.ProviderType == AuthProviderType.Password) ?? throw new AppException("Password authentication provider is not available.");

        var authenticationRequest = new AuthenticationRequest
        {
            Identifier = request.UsernameOrEmail,
            Secret = request.Password,
        };

        var result = await provider.AuthenticateAsync(authenticationRequest, cancellationToken);

        if (!result.Succeeded || result.User is null)
            throw new AppException(result.Error ?? "Invalid credentials.", 401);

        if (result.User.Status is UserStatus.Suspended)
            throw new AppException("User account is Suspended.", 403);

        if (result.User.Status is UserStatus.Deactivated)
            throw new AppException("User account is Deactivated.", 403);

        if (result.User.Status is UserStatus.Deleted)
            throw new AppException("User account is Deleted.", 403);

        var (token, expriesAt) = _tokenService.GenerateAccessToken(result.User);

        return new AuthResponseDto(token, expriesAt, ToDto(result.User));
    }
    private static UserDto ToDto(User user)
    {
        UserDto userDto = new UserDto(
            user.Id,
            user.Username,
            user.FirstName,
            user.LastName,
            user.Email,
            user.IsEmailVerified,
            user.PhoneCountryCode,
            user.PhoneNumber,
            user.IsPhoneVerified,
            user.Birthday,
            user.Status.ToString(),
            user.CreatedAt,
            user.UpdatedAt
        );

        return userDto;
    }
    
}
