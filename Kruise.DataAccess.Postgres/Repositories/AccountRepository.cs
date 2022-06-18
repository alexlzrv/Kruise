using AutoMapper;
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

    public async Task<long> Add(Domain.AccountModel newAccount)
    {
        var account = new Entities.AccountEntity(0, newAccount.Name);
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

    public async Task<Domain.AccountModel[]> Get()
    {
        var account = await _dbContext.Accounts.AsNoTracking().ToArrayAsync();
        return _mapper.Map<Entities.AccountEntity[], Domain.AccountModel[]>(account);
    }

    public async Task<Domain.AccountModel?> Get(long accountId)
    {
        var account = await _dbContext.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == accountId);
        if (account == null)
        {
            return null;
        }

        return _mapper.Map<Entities.AccountEntity, Domain.AccountModel>(account);
    }
}
