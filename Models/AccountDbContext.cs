using Microsoft.EntityFrameworkCore;

namespace FadeFactory_Accounts.Models;

public class AccountDbContext : DbContext
{
    public AccountDbContext() { }
    public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options) { }
    public virtual DbSet<Account> Accounts { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultContainer("Accounts");
        builder.Entity<Account>().HasNoDiscriminator();
        builder.Entity<Account>().ToContainer("Accounts");
    }
}

