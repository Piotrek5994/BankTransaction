using Microsoft.AspNetCore.Mvc;

namespace BankTransaction.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
