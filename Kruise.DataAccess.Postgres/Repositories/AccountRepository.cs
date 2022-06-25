using AutoMapper;
using CSharpFunctionalExtensions;
using Kruise.DataAccess.Postgres.Entities;
using Kruise.Domain;
using Kruise.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kruise.DataAccess.Postgres.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly KruiseDbContext _dbContext;
    private readonly IMapper _mapper;

    public AccountRepository(KruiseDbContext dbContext, Mapper mapper)
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
