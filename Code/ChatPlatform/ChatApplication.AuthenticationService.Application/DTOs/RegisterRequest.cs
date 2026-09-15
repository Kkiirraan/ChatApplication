using System.ComponentModel.DataAnnotations;

namespace ChatApplication.AuthenticationService.Application.DTOs;

public class RegisterRequest
{
    [Required(ErrorMessage = "Username is required.")] 
    [MinLength(3, ErrorMessage = "Username must be at least 3 characters.")] 
    [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
    public string Username { get; set; } = default!;

    [Required(ErrorMessage = "First name is required.")]
    [MaxLength(100, ErrorMessage = "First name cannot exceed 100 characters.")]
    public string FirstName { get; set; } = default!;

    [Required(ErrorMessage = "Last name is required.")]
    [MaxLength(100, ErrorMessage = "Last name cannot exceed 100 characters.")]
    public string LastName { get; set; } = default!;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    [MaxLength(256)]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Phone country code is required.")]
    [RegularExpression(@"^\+[1-9]\d{0,3}$", ErrorMessage = "PhoneCountryCode must be like +91.")]
    public string PhoneCountryCode { get; set; } = default!;

    [Required(ErrorMessage = "Phone number is required.")]
    [RegularExpression(@"^\d{4,14}$", ErrorMessage = "PhoneNumber must contain only digits.")]
    public string PhoneNumber { get; set; } = default!;

    [Required(ErrorMessage = "Birthday is required.")]
    public DateOnly? Birthday { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
    [MaxLength(128, ErrorMessage = "Password cannot exceed 128 characters.")]
    public string Password { get; set; } = default!;
}
