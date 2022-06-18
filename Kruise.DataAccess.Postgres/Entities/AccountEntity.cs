using System.ComponentModel.DataAnnotations;
using Kruise.Domain;

namespace Kruise.DataAccess.Postgres.Entities;

public class AccountEntity
{
    public AccountEntity(long id, string name)
    {
        Id = id;
        Name = name;
    }

    public long Id { get; set; }

    [StringLength(AccountModel.MaxNameLength)]
    public string Name { get; set; }

}
