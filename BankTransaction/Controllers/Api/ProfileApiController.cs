using BankTransaction.Controllers.Repositores.Interface;
using BankTransaction.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BankTransaction.Controllers.Api
{
    [Route("api/Profiles")]
    [ApiController]
    public class ProfileApiController : ControllerBase
    {
        private readonly TransactionDbContext _context;
        private readonly IProfieService _profileService;

        public ProfileApiController(IProfieService profileService , TransactionDbContext context)
        {
            _context = context;
            _profileService = profileService;
        }

        [HttpPut]
        public IActionResult AddMoney(decimal amonunt)
        {
            var user = _context.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
            {
                return NotFound();
            }
            _profileService.AddMoney(user, amonunt);
            return new OkObjectResult(user);
        }
        [HttpPatch]
        public IActionResult ConvertToEuro(decimal amount)
        {
           
            var user = _context.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
            {
                return NotFound();
            }
            _profileService.ConvertToEuro(user, amount);
            return new OkObjectResult(user);
        }
        [HttpPatch]
        public IActionResult ConvertToDolar(decimal amount)
        {
            
            var user = _context.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
            {
                return NotFound();
            }
            _profileService.ConvertToDolar(user, amount);
            return new OkObjectResult(user);
        }
    }
}
