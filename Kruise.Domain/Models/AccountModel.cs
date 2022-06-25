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
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<AccountModel>(Errors.Account.NameCanNotBeNullOrWhiteSpace);
        }

        if (name.Length > MaxNameLength)
        {
            var error = string.Format(Errors.Account.NameMaxLength, MaxNameLength);
            return Result.Failure<AccountModel>(error);
        }

        return new AccountModel(0, name);
    }
}
