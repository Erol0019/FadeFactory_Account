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
        builder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId);
            entity.Property(e => e.AccountId).ValueGeneratedOnAdd();
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.PasswordSalt).IsRequired();
            entity.Property(e => e.IsPromotional).IsRequired();
            entity.Property(e => e.IsAdmin).IsRequired();
            entity.HasNoDiscriminator();
            entity.ToContainer("Accounts");
        });
    }
}

