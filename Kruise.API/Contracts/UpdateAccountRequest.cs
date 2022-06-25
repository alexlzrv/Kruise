using System.ComponentModel.DataAnnotations;
using Kruise.Domain;

namespace Kruise.API.Contracts
{
    public class UpdateAccountRequest
    {
        public UpdateAccountRequest(string name)
        {
            Name = name;
        }

        [Required]
        [StringLength(AccountModel.MaxNameLength)]
        public string Name { get; set; }
    }
}
