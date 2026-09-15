using ChatApplication.AuthenticationService.Application.Interfaces;
using ChatApplication.AuthenticationService.Application.Providers;
using ChatApplication.AuthenticationService.Application.Services;
using ChatApplication.AuthenticationService.Infrastructure.Persistence;
using ChatApplication.AuthenticationService.Infrastructure.Repositories;
using ChatApplication.AuthenticationService.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApplication.AuthenticationService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("AuthenticationDatabase")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, JwtTokenService>();

        services.AddScoped<IAuthenticationProvider, PasswordAuthenticationProvider>();

        return services;
    }
}
