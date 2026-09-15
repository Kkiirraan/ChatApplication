
using ChatApplication.AuthenticationService.Domain.Enums;

namespace ChatApplication.AuthenticationService.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public bool IsEmailVerified { get; set; }
    public string PhoneCountryCode { get; set; } = default!; // e.g. "+91"
    public string PhoneNumber { get; set; } = default!;
    public bool IsPhoneVerified { get; set; }
    public DateOnly Birthday { get; set; }
    public UserStatus Status { get; set; } = UserStatus.PendingVerification;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // A user can have multiple ways to authenticate (password today, Google/OTP/etc. tomorrow)
    public ICollection<UserCredential> Credentials { get; set; } = new List<UserCredential>();
}

