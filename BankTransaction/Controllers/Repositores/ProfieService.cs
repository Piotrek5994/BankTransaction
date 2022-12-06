using BankTransaction.Controllers.Repositores.Interface;
using BankTransaction.Models;

namespace BankTransaction.Controllers.Repositores
{
    public class ProfieService : IProfieService
    {
        private readonly TransactionDbContext _context;

        public ProfieService(TransactionDbContext context)
        {
            _context = context;
        }

        public void AddMoney(User user,decimal amount)
        {
            user.AccountBalance += amount;
            _context.Update(user);
            _context.SaveChanges();
        }
    }
    
}
