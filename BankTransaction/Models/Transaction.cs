﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankTransaction.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string AccountNumber { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string BeneficiaryName { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string BankName { get; set; }
        [Column(TypeName = "nvarchar(11)")]
        public string SWIFTCode { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
    }
}
