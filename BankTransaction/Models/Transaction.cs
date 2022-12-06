using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BankTransaction.Models;

public class Transaction
{
    [Key] 
    public int TransactionId { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    [DisplayName("Sender Email")]
    public string SenderEmail { get; set; }

    [Column(TypeName = "nvarchar(16)")]
    [Display(Name = "Account Number")]
    [Required(ErrorMessage = "This field is required.")]
    [MaxLength(16, ErrorMessage = "Maximum length is 16.")]
    public string AccountNumber { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    [Display(Name = "Beneficiary Name")]
    [Required(ErrorMessage = "This field is required.")]
    public string BeneficiaryName { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    [Display(Name = "Bank Name")]
    [Required(ErrorMessage = "This field is required.")]
    public string BankName { get; set; }

    [Column(TypeName = "nvarchar(11)")]
    [Display(Name = "SWIFT Code ")]
    [Required(ErrorMessage = "This field is required.")]
    [MaxLength(11, ErrorMessage = "Maximum length is 11.")]
    public string SWIFTCode { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [DataType(DataType.Currency)]
    public decimal? Amount { get; set; }
    
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime Date { get; set; }
    // Tworzenie relacji wiele do jednego że każda transakcja musi miec 1 użytkownika

    [ValidateNever]
    public User User { get; set; }

    //public Transaction Clone()
    //{
    //    return (Transaction)this.MemberwiseClone();
    //}
}