namespace ChatApplication.AuthenticationService.Application.DTOs;

public record UserDto(
    Guid Id,
    string Username,
    string FistName,
    string LastName,
    string Email,
    bool IsEmailVerified,
    string PhoneCountryCode,
    string PhoneNumber,
    bool IsPhoneVerified,
    DateOnly Birthday,
    string status,
    DateTime CreatedAt,
    DateTime UpdatedAt
    );
