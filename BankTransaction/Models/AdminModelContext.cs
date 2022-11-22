using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankTransaction.Models;

public class AdminModelContext : IdentityDbContext<User>
{
    public AdminModelContext(DbContextOptions<TransactionDbContext> options) : base(options)
    {

    }
    public DbSet<Transaction> Transaction { get; set; }

    public DbSet<User> User { get; set; }

} 
