using BankTransaction.Controllers.Repositores.Interface;
using BankTransaction.Models;
using Microsoft.AspNetCore.Mvc;

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
        [Fact]
        public void Test4()
        {
            //given
            var transactionController = new TestTransactionController();
            //when
            var result = transactionController.AddOrEdit(null);
            //then
            Assert.True(result.Result is NotFoundResult);
        }
        [Fact]
        public void Test5()
        {
            //given
            var transaction = new Transaction
            {
                Amount = 100,
                SenderEmail = "Misiek5994@gmail.com",
                AccountNumber = "1670089534760785",
                BankName = "PKO BP",
                SWIFTCode = "11111111111",
                BeneficiaryName = "Misiek",
            };
            var transactionController = new TestTransactionController();
            //when
            var result = transactionController.AddOrEdit(transaction);
            //then
            Assert.True(result.Result is OkObjectResult);
        }
        [Fact]
        public void Test6()
        {
            //given
            var transactionController = new TestTransactionController();
            //when
            var result = transactionController.DeleteConfirmed(0);
            //then
            Assert.True(result.Result is NotFoundResult);
        }
        [Fact]
        public void Test7()
        {
            //given
            var transaction = new Transaction
            {
                Amount = 100,
                SenderEmail = "Random5994@gmail.com",
                AccountNumber = "1670089534760785",
                BankName = "PKO BP",
                SWIFTCode = "11111111111",
                BeneficiaryName = "Misiek",
            };
            var transactionController = new TestTransactionController();
            var task = transactionController.AddOrEdit(transaction);
            OkObjectResult res = (OkObjectResult)task.Result;
            Transaction? t = res.Value as Transaction;
            //when  
            var result = transactionController.DeleteConfirmed(t.TransactionId);
            //then
            Assert.True(result.Result is OkObjectResult);
        }
    }
}