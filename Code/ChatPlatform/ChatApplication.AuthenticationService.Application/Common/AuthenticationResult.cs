using ChatApplication.AuthenticationService.Domain.Entities;

namespace ChatApplication.AuthenticationService.Application.Common;

public class AuthenticationResult
{
    public bool Succeeded { get; init; }
    public User? User { get; init; }
    public string? Error { get; init; }

    public static AuthenticationResult Success(User user) => new () { Succeeded = true, User = user  };
    public static AuthenticationResult Fail(string error) => new () { Succeeded = false, Error = error };
}
