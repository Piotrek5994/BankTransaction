using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankTransaction.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace BankTransaction.Controllers
{
    [Authorize(Policy = "Admin")]
    public class AdminController : Controller
    {

        private readonly TransactionDbContext _context;
        public AdminController(TransactionDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // stworzenie krotki z danymi do wyświetlenia
            Tuple<List<Transaction>, List<User>> tuple = new Tuple<List<Transaction>, List<User>>(_context.Transactions.ToList(), _context.Users.ToList());
            return View(tuple);
        }
        
       

    }
}
