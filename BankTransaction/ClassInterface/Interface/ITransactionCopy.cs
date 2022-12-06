using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace BankTransaction.ClassInterface.Interface
{
    public interface ITransactionCopy
    {
        IActionResult Create(Transaction? transaction);
        IActionResult AddOrEdit(int? id = 0);
        IActionResult DeleteConfirmed(int? id);

    }
}
