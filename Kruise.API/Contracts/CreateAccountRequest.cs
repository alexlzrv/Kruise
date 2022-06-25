using System.ComponentModel.DataAnnotations;
using Kruise.Domain;

namespace Kruise.API.Contracts;

public class CreateAccountRequest
{
    public CreateAccountRequest(string name)
    {
        Name = name;
    }

    [Required(ErrorMessage = Errors.Account.NameCanNotBeNullOrWhiteSpace)]
    [StringLength(AccountModel.MaxNameLength, ErrorMessage = Errors.Account.NameMaxLength)]
    public string Name { get; set; }
}
