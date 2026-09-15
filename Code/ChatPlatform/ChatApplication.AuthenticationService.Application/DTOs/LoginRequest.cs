using System.ComponentModel.DataAnnotations;

namespace ChatApplication.AuthenticationService.Application.DTOs;

public class LoginRequest
{
    [Required]
    public string UsernameOrEmail { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}
