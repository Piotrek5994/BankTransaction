using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankTransaction.Models;

[Index(nameof(AccountNumber), IsUnique = true)]
public class User : IdentityUser
{
    [DataType(DataType.Currency)]
    public decimal AccountBalance { get; set; }

    [Column(TypeName = "nvarchar(16)")]
    public string AccountNumber { get; set; }

    // tworzenie relacjo wiele do wielu
    public ICollection<Transaction> Transactions { get; set; }

    public User(): base()
    {
        //losowanie numeru konta 
        var random = new Random();
        string salt = random.Next(100, 999).ToString();
        string time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
        
        AccountNumber = time + salt;
    }
}