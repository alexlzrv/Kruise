using System.ComponentModel.DataAnnotations;

namespace Kruise.API.Contracts;

public class LoginRequest
{
    [EmailAddress]
    public string Email { get; set; }

    public string Password { get; set; }

    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }
}
