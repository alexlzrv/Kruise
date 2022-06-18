using Kruise.Domain;
using System.ComponentModel.DataAnnotations;

namespace Kruise.API.Contracts;

public class CreateAccountRequest
{
    public CreateAccountRequest(string name)
    {
        Name = name;
    }

    [Required]
    [StringLength(AccountModel.MaxNameLength)]
    public string Name { get; set; }
}
