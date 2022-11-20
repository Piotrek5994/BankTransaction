using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BankTransaction.Models
{
    public class User : IdentityUser
    {
        [DataType(DataType.Currency)]
        public decimal AccountBalance { get; set; }
        
    }

}
