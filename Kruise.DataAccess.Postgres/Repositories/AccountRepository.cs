using AutoMapper;
using CSharpFunctionalExtensions;
using Kruise.DataAccess.Postgres.Entities;
using Kruise.Domain;
using Kruise.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Kruise.DataAccess.Postgres.Repositories;
public class AccountCacheRepository : IAccountRepository
{
    private readonly IAccountRepository _repository;
    private readonly IMemoryCache _cache;
    private const string Key = "Accounts";

    public AccountCacheRepository(IAccountRepository repository, IMemoryCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public Task<long> Add(AccountModel newAccount)
    {
        return _repository.Add(newAccount);
    }

    public async Task<AccountModel[]> Get()
    {
        var accounts = await _cache.GetOrCreateAsync<AccountModel[]>(Key, entry =>
        {
            return _repository.Get();
        });
        return accounts;
    }

    public Task<AccountModel?> Get(long accountId)
    {
        return _repository.Get(accountId);
    }

    public Task Remove(long accountId)
    {
        return _repository.Remove(accountId);
    }

    public Task<Result> Update(long accountId, AccountModel account)
    {
        return _repository.Update(accountId, account);
    }
}

public class AccountRepository : IAccountRepository
{
    private readonly KruiseDbContext _dbContext;
    private readonly IMapper _mapper;

    public AccountRepository(KruiseDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<long> Add(AccountModel newAccount)
    {
        var account = new AccountEntity(0, newAccount.Name);
        _dbContext.Accounts.Add(account);
        await _dbContext.SaveChangesAsync();
        return account.Id;
    }

    public async Task Remove(long accountId)
    {
        var account = await _dbContext.Accounts.FindAsync(accountId);
        _dbContext.Accounts.Remove(account);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<AccountModel[]> Get()
    {
        var account = await _dbContext.Accounts.AsNoTracking().ToArrayAsync();
        return _mapper.Map<AccountEntity[], AccountModel[]>(account);
    }

    public async Task<AccountModel?> Get(long accountId)
    {
        var account = await _dbContext.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == accountId);
        if (account == null)
        {
            return null;
        }

        return _mapper.Map<AccountEntity, AccountModel>(account);
    }

    public async Task<Result> Update(long acccountId, AccountModel account)
    {
        var accountExists = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Id == acccountId);
        if (accountExists == null)
        {
            return Result.Failure($"Post with id: {acccountId} not found");
        }

        var accountEntity = _mapper.Map(account, accountExists);
        await _dbContext.SaveChangesAsync();
        return Result.Success();
    }
}
