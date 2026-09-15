
using ChatApplication.AuthenticationService.Domain.Enums;

namespace ChatApplication.AuthenticationService.Domain.Entities;

public class UserCredential
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    public AuthProviderType Provider { get; set; }
    public string Identifier { get; set; } = default!;
    public string? SecretHash { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

}

