using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace BankTransaction.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        [Column(TypeName = "nvarchar(12)")]
        [Display(Name ="Account Number")]
        public string AccountNumber { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Beneficiary Name")]
        public string BeneficiaryName { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }
        [Column(TypeName = "nvarchar(11)")]
        [Display(Name = "SWIFT Code ")]
        public string SWIFTCode { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
    }
}
