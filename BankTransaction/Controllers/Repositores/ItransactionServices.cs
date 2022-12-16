using BankTransaction.Controllers.Repositores.Interface;
using BankTransaction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankTransaction.Controllers.Repositores
{
    public class ItransactionServices : ITransactionController
    {
        private readonly TransactionDbContext _context;

        public ItransactionServices(TransactionDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AddOrEdit([Bind("TransactionId,SenderEmail,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] Transaction transaction)
        {
            if (transaction == null)
            {
                if (transaction.TransactionId == 0)
                {
                    _context.Add(transaction);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    try
                    {
                        _context.Update(transaction);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TransactionExists(transaction.TransactionId))

                            return new ObjectResult("Transaction not found");

                        else
                            throw;
                    }
                }
                return new OkObjectResult(transaction);
            }
            return new BadRequestObjectResult(transaction);
        }

        public Task<IActionResult> AddOrEdit([Bind(new[] { "TransactionId,SenderEmail,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date" })] System.Transactions.Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return new OkObjectResult(transaction);
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }
    }
    
}
