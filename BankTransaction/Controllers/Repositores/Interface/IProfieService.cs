using BankTransaction.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankTransaction.Controllers.Repositores.Interface
{
    public interface IProfieService
    {
        public void AddMoney(User user , decimal amount);

        public void ConvertToEuro(User user, decimal amount);

        public void ConvertToDolar(User user, decimal amount);

        public void EuroToPln(User user, decimal amount);

        public void UsdToPln(User user, decimal amount);
    }
}
