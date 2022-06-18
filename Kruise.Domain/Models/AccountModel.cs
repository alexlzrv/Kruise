using CSharpFunctionalExtensions;

namespace Kruise.Domain;
public record AccountModel
{
    public const int MaxNameLength = 30;

    public AccountModel(long id, string name)
    {
        Id = id;
        Name = name;
    }

    public long Id { get; }
    public string Name { get; }

    public static Result<AccountModel> Create(string name)
    {
        return new AccountModel(0, name);
    }

}
