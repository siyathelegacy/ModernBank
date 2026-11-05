using Microsoft.EntityFrameworkCore;
using ModernBank.Application.Data;
using ModernBank.Domain.Models.AccountManagement;

namespace ModernBank.Infrastructure.Data;

public sealed class ModernBankDbContext(DbContextOptions<ModernBankDbContext> options)
    : DbContext(options), IDbContext
{
    public DbSet<UserAccount> Accounts => Set<UserAccount>();
    public DbSet<BankAccount> Banks => Set<BankAccount>();
    public DbSet<Transaction> Transactions => Set<Transaction>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ModernBankDbContext).Assembly);

        modelBuilder.Entity<UserAccount>( e =>
        {
            e.ToTable("UserAccounts");
            e.HasKey(ua => ua.UserId);

            e.Property(ua => ua.FullName).HasMaxLength(200);
            e.Property(ua => ua.Email).HasMaxLength(256);
            e.Property(ua => ua.MobileNumber).HasMaxLength(30);
            e.Property(ua => ua.IdentityNumber).HasMaxLength(50);
            e.Property(ua => ua.OrganizationName).HasMaxLength(200);
            e.Property(ua => ua.TaxReference).HasMaxLength(50);
            e.Property(ua => ua.CustomerType).HasConversion<string>().HasMaxLength(30);
            e.Property(ua => ua.IdentityType).HasConversion<string>().HasMaxLength(30);

            e.HasIndex(ua => ua.Email).IsUnique();

            e.HasMany(ua => ua.BankAccounts)
             .WithOne(ba => ba.UserAccount)
             .HasForeignKey(ba => ba.UserId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<BankAccount>(e =>
        {
            e.ToTable("BankAccounts");
            e.HasKey(ba => ba.BankAccId);

            e.Property(ba => ba.Name).HasMaxLength(100);
            e.Property(ba => ba.AccountNumber).HasMaxLength(30);
            e.Property(ba => ba.Type).HasMaxLength(30);
            e.Property(ba => ba.Balance).HasPrecision(18, 2);

            e.HasIndex(ba => ba.AccountNumber).IsUnique();

            // One bank account -> many transactions
            e.HasMany(ba => ba.Transactions)
             .WithOne(t => t.BankAccount)
             .HasForeignKey(t => t.BankAccId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Transaction>(e =>
        {
            e.ToTable("Transactions");
            e.HasKey(t => t.TransactionId);

            e.Property(t => t.Type).HasMaxLength(30);
            e.Property(t => t.Destination).HasMaxLength(200);
            e.Property(t => t.Amount).HasPrecision(18, 2);
        });

    }
}
