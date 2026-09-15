using ChatApplication.AuthenticationService.Domain.Entities;
using ChatApplication.AuthenticationService.Domain.Enums;
using System.Net;

namespace ChatApplication.AuthenticationService.Application.Interfaces;

public interface IUserRepository
{
    Task<bool> ExistsAsync(string username, string email, CancellationToken cancellationToken = default);
    Task AddAsync(User user, CancellationToken cancellationToken = default);
    Task AddCredentialAsync(UserCredential credential, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<User?> GetByUsernameOrEmailAsync(string usernameOrEmail, CancellationToken cancellationToken = default);
    Task<UserCredential?> GetCredentialAsync(Guid userId, AuthProviderType provider, CancellationToken cancellationToken = default);


}
