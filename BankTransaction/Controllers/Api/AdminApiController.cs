using BankTransaction.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankTransaction.Controllers
{
    [Route("api/Admins")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly TransactionDbContext _context;

        public AdminApiController(TransactionDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            Tuple<List<Transaction>, List<User>> tuple = new Tuple<List<Transaction>, List<User>>(_context.Transactions.ToList(), _context.Users.ToList());
            return new OkObjectResult(tuple);
        }
      
    }
}
