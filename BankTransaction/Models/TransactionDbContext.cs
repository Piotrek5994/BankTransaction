using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankTransaction.Models;

public class TransactionDbContext : IdentityDbContext<User>
{
    public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
    {
           
    }

    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        //ustawienie długośc konta na 250 znaków
        modelBuilder.Entity<User>()
            .Property(e => e.AccountBalance)
            .HasMaxLength(250);

    }
}