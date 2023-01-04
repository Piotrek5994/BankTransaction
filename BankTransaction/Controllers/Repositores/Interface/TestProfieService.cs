using BankTransaction.Models;
namespace BankTransaction.Controllers.Repositores.Interface
{
    public class TestProfieService : IProfieService
    {
        public void AddMoney(User user, decimal amount)
        {
            user.AccountBalance += amount;
        }
        public void ConvertToEuro(User user, decimal amount)
        {
            user.AccountBalance = amount * (decimal)0.21;
        }
        public void ConvertToDolar(User user, decimal amount)
        {
            user.AccountBalance = amount * (decimal)0.25;
        }

        public void EuroToPln(User user, decimal amount)
        {
            throw new NotImplementedException();
        }

        public void UsdToPln(User user, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
