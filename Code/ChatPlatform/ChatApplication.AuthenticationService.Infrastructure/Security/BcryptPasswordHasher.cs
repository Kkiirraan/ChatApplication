using ChatApplication.AuthenticationService.Application.Interfaces;

namespace ChatApplication.AuthenticationService.Infrastructure.Security;

public class BcryptPasswordHasher : IPasswordHasher
{   
    public string Hash(string password)
    { 
        return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
    }
    public bool Verify(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
