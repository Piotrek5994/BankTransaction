using BankTransaction.Controllers;
using BankTransaction.Models;
using System.Security.Cryptography.X509Certificates;

namespace Transaction_test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var user = new User();
            var amount = 100;
            var profileController = new ProfileController();
            //When
            profileController.AddMoney(user, amount);
            //Then
            Assert.Equal(100, user.AccountBalance);
        }
    }
}