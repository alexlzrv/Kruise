using Kruise.DataAccess.Postgres.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kruise.DataAccess.Postgres;

public class KruiseDbContext : IdentityDbContext
{
    public KruiseDbContext(DbContextOptions<KruiseDbContext> options)
        : base(options)
    {
    }

    public DbSet<PostEntity> Posts { get; set; } = null!;

    public DbSet<AccountEntity> Accounts { get; set; } = null!;
}
