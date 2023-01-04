using BankTransaction.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankTransaction.Controllers.Repositores.Interface;

public interface ITransactionController
{
    public Task<IActionResult> DeleteConfirmed(int id);

    public Task<IActionResult> AddOrEdit([Bind("TransactionId,SenderEmail,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] Transaction transaction);
}
