using BankTransaction.Controllers.Repositores.Interface;
using BankTransaction.Models;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //given
            var user = new User();
            var amount = 100;
            var profileService = new TestProfieService();
            //when
            profileService.AddMoney(user, amount);
            //then
            Assert.Equal(100, user.AccountBalance);
        }
        [Fact]
        public void Test2()
        {
            //given
            var user = new User();
            var amount = 100;
            var profileService = new TestProfieService();
            //when
            profileService.ConvertToEuro(user, amount);
            //then
            Assert.Equal(21, user.AccountBalance);
        }
        [Fact]
        public void Test3()
        {
            //given
            var user = new User();
            var amount = 100;
            var profileService = new TestProfieService();
            //when
            profileService.ConvertToDolar(user, amount);
            //then
            Assert.Equal(25, user.AccountBalance);
        }
    }
}