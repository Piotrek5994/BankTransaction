using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankTransaction.Models;

public class TransactionDbContext : IdentityDbContext<IdentityUser>
{
    public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
    {
    }

    public DbSet<Transaction> Transactions { get; set; }
}