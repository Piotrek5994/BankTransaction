using BankTransaction.Models;

namespace BankTransaction.Controllers.Repositores.Interface
{
    public interface IProfieService
    {
        public void AddMoney(User user , decimal amount);

    }
}
