using BankTransaction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankTransaction.Controllers.Repositores.Interface;

public class TestTransactionController : ITransactionController
{
    private readonly TransactionDbContext _context;

    public TestTransactionController()
    {
        _context = new TransactionDbContext(
            new DbContextOptionsBuilder<TransactionDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options
        );
    }

    public async Task<IActionResult> AddOrEdit([Bind(new[] { "TransactionId,SenderEmail,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date" })] Transaction? transaction)
    {
        if (transaction == null)
            return new NotFoundResult();

        if (transaction.TransactionId == 0)
            _context.Add(transaction);
        else
            _context.Update(transaction);

        await _context.SaveChangesAsync();

        return new OkObjectResult(transaction);
    }

    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null)
            return new NotFoundResult();
        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
        return new OkObjectResult(transaction);
    }
}
