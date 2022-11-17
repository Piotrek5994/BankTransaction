using Microsoft.EntityFrameworkCore;

namespace BankTransaction.Models
{
    public class TramsactionDbContext : DbContext
    {
        public TramsactionDbContext(DbContextOptions<TramsactionDbContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
    }
    
    
}
