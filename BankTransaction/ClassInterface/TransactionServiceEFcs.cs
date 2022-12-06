using BankTransaction.ClassInterface.Interface;
using BankTransaction.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BankTransaction.ClassInterface
{
    public class TransactionServiceEFcs : ITransactionCopy
    {
        private readonly TransactionDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TransactionServiceEFcs(TransactionDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Create(System.Transactions.Transaction transaction)
        {
            _context.Add(transaction);
            _context.SaveChanges();
            return new OkObjectResult(transaction);
        }

        public IActionResult AddOrEdit(int? id = 0)
        {
            
            if (id == 0)
                return new OkObjectResult(new Transaction());
            else
                return new OkObjectResult(_context.Transactions.Find(id));
        }
        
        public IActionResult DeleteConfirmed(int? id)
        {
            var transaction = _context.Transactions.Find(id);
            _context.Transactions.Remove(transaction);
            _context.SaveChanges();
            return new OkResult();
        }

    }
}
