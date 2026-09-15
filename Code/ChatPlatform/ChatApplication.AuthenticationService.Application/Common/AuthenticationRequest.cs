namespace ChatApplication.AuthenticationService.Application.Common;

public class AuthenticationRequest
{
    public string Identifier { get; set; } = default!;
    public string? Secret { get; set; }
    public Dictionary<string, string>? Metadata { get; set; }
}
