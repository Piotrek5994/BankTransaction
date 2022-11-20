using BankTransaction.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankTransaction.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly TransactionDbContext _context;

    public ProfileController(TransactionDbContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        var user = _context.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));
        return View(user);
    }

    [HttpGet()]
    public IActionResult AddMoney([FromQuery(Name = "amount")] decimal amount)
    {
        var user = _context.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));
        user.AccountBalance += amount;
        _context.Update(user);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Transaction()
    {
        return View();
    }
}
