using BankTransaction.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BankTransaction.Controllers;

[Authorize]
public class TransactionsController : Controller
{
    private readonly TransactionDbContext _context;

    public TransactionsController(TransactionDbContext context)
    {
        _context = context;
    }

    // GET: Transactions
    public async Task<IActionResult> Index()
    {
        var transactions = await _context.Transactions
            .Where(x => x.SenderEmail == User.FindFirstValue(ClaimTypes.Email))
            .ToListAsync();
        return View(transactions);
    }

    // GET: Transactions/Create
    public IActionResult AddOrEdit(int id = 0)
    {
        if (id == 0)
            return View(new Transaction());
        else
            return View(_context.Transactions.Find(id));
    }

    // POST: Transactions/AddOrEdit
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrEdit([Bind("TransactionId,SenderEmail,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] Transaction transaction)
    {
        if (ModelState.IsValid)
        {
            if (transaction.TransactionId == 0)
            {
                var user = _context.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (user.AccountBalance < transaction.Amount)
                {
                    ModelState.AddModelError("Amount", "You don't have enough money");
                    return View(transaction);
                }
                if (transaction.Amount < 0.01m)
                {
                    ModelState.AddModelError("Amount", "You can't send less than 0.01");
                    return View(transaction);
                }

                user.AccountBalance -= transaction.Amount ?? 0;

                transaction.Date = DateTime.Now;
                transaction.SenderEmail = user.Email;

                _context.Update(user);
                _context.Add(transaction);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(transaction);
    }

    // POST: Transactions/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Transactions == null)
        {
            return Problem("Entity set 'TramsactionDbContext.Transactions'  is null.");
        }
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction != null)
        {
            _context.Transactions.Remove(transaction);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}