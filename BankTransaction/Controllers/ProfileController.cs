using BankTransaction.Controllers.Repositores.Interface;
using BankTransaction.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Security.Claims;

namespace BankTransaction.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly TransactionDbContext _context;
    private readonly IProfieService _profileService;

    public ProfileController(TransactionDbContext context, IProfieService profileService)
    {
        _context = context;
        _profileService = profileService;
    }
    
    public IActionResult Index()
    {
        var user = _context.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));
        return View(user);
    }

    [HttpGet()]
    public IActionResult AddMoney([FromQuery(Name = "amount")] decimal amount)
    {
        // dodawanie pieniedzy do użytkownika
        var user = _context.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (user == null)
        {
            return NotFound();
        }
        //user.AccountBalance += amount;
        //_context.Update(user);
        //_context.SaveChanges();
        _profileService.AddMoney(user, amount);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Transaction()
    {
        return View();
    }
    public IActionResult ConvertToEuro([FromQuery(Name = "amount")] decimal amount)
    {
        // konwertowanie pieniedzy na euro
        var user = _context.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (user == null)
        {
            return NotFound();
        }
        //user.AccountBalance = amount * (decimal)0.21;
        //_context.Update(user);
        //_context.SaveChanges();
        _profileService.ConvertToEuro(user, amount);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult ConvertToDolar([FromQuery(Name = "amount")] decimal amount)
    {
        // konwertowanie pieniedzy na dolary
        var user = _context.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (user == null)
        {
            return NotFound();
        }
        //user.AccountBalance = amount * (decimal)0.22;
        //_context.Update(user);
        //_context.SaveChanges();
        _profileService.ConvertToDolar(user, amount);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult EuroToPln([FromQuery(Name = "amount")] decimal amount)
    {
        // konwertowanie pieniedzy z euro na pln
        var user = _context.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (user == null)
        {
            return NotFound();
        }
        //user.AccountBalance = amount / (decimal)0.21;
        //_context.Update(user);
        //_context.SaveChanges();
        _profileService.EuroToPln(user, amount);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult UsdToPln([FromQuery(Name = "amount")] decimal amount)
    {
        // konwertowanie pieniedzy z dolara na pln
        var user = _context.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (user == null)
        {
            return NotFound();
        }
        //user.AccountBalance = amount / (decimal)0.22;
        //_context.Update(user);
        //_context.SaveChanges();
        _profileService.UsdToPln(user, amount);
        return RedirectToAction(nameof(Index));
    }


}
