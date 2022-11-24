using BankTransaction.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BankTransaction.Controllers;

[Authorize]
public class TransactionsController : Controller
{
    private readonly TransactionDbContext _context;
    private readonly UserManager<User> _userManager;    

    public TransactionsController(TransactionDbContext context, UserManager<User> manager)
    {
        _context = context;
        _userManager = manager;
    }

    // GET: Transactions
    public async Task<IActionResult> Index()
    {
        
        var abc = await _context.Transactions.Include(t => t.User).ToListAsync();
        //tworzenie osobnych profili przy zakładaniu konta
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
        if (!ModelState.IsValid || transaction.TransactionId != 0)
            return View(transaction);

        // pobieranie aktualnego użytkownika i dane tego który dostanie przelew
        var user = _context.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var receivingUser = _context.Users.FirstOrDefault(x => x.AccountNumber == transaction.AccountNumber);

        if (receivingUser == null)
        {
            ModelState.AddModelError("AccountNumber", "Account number is not valid.");
            return View(transaction);
        }
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
        // odjecie sumy po transakcji ze stanu konta
        user.AccountBalance -= transaction.Amount ?? 0;
        // dodanie sumy pienidzy po transakcji do drugiego użytkownika
        receivingUser.AccountBalance += transaction.Amount ?? 0;

        // transakcja przychodząca
        var receivingTransaction = new Transaction()
        {
            SenderEmail = receivingUser.Email,
            AccountNumber = user.AccountNumber,
            BeneficiaryName = transaction.BeneficiaryName,
            BankName = transaction.BankName,
            SWIFTCode = transaction.SWIFTCode,
            Amount = transaction.Amount,
            Date = DateTime.Now,
            User = await _userManager.GetUserAsync(HttpContext.User)
        };

        transaction.Date = DateTime.Now;
        transaction.SenderEmail = user.Email;
        // utworzenie odjecia z konta widok
        transaction.Amount = -transaction.Amount;

        _context.Update(user);
        // update stanu konta osoby odbierającej przelew
        _context.Update(receivingUser);
        _context.Add(transaction);
        // dodanie widoku transakcji dla osoby przychodzącej
        _context.Add(receivingTransaction);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
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