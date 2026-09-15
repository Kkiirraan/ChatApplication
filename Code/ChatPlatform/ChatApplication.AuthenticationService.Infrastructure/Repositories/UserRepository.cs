using ChatApplication.AuthenticationService.Application.Interfaces;
using ChatApplication.AuthenticationService.Domain.Entities;
using ChatApplication.AuthenticationService.Domain.Enums;
using ChatApplication.AuthenticationService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.AuthenticationService.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    public UserRepository(AppDbContext db)
    {
        _db = db;
    }
    public Task<User?> GetByUsernameOrEmailAsync(string usernameOrEmail, CancellationToken cancellationToken)
    {
        return _db.Users.FirstOrDefaultAsync(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail.ToLower(), cancellationToken);
    }
    public Task<UserCredential?> GetCredentialAsync(Guid userId, AuthProviderType provider, CancellationToken cancellationToken)
    {
        return _db.UserCredentials.FirstOrDefaultAsync(c => c.UserId == userId && c.Provider == provider, cancellationToken);
    }
    public Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        _db.Users.Add(user);
        return Task.CompletedTask;
    }

    public Task AddCredentialAsync(UserCredential credential, CancellationToken cancellationToken = default)
    {
        _db.UserCredentials.Add(credential);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(string username, string email, CancellationToken cancellationToken = default)
    {
        return _db.Users.AnyAsync(u => u.Username == username || u.Email == email, cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _db.SaveChangesAsync();
    }
}
