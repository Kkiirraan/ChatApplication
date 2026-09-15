
namespace ChatApplication.AuthenticationService.Domain.Enums
{
    public enum UserStatus
    {
        PendingVerification = 0,
        Active = 1,
        Suspended = 2,
        Deactivated = 3,
        Deleted = 4
    }
}
