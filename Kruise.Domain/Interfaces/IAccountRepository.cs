using CSharpFunctionalExtensions;

namespace Kruise.Domain.Interfaces;
public interface IAccountRepository
{
    Task<long> Add(AccountModel newAccount);

    Task Remove(long accountId);

    Task<AccountModel[]> Get();

    Task<AccountModel?> Get(long accountId);

    Task<Result> Update(long accountId, AccountModel account);
}
