using Kruise.DataAccess.Postgres.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kruise.DataAccess.Postgres;

public class KruiseDbContext : DbContext
{
    public KruiseDbContext(DbContextOptions<KruiseDbContext> options)
        : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; } = null!;
}
