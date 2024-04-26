using ShowWork.BL.Auth;
using ShowWorkTests.Helpers;
using System.Transactions;

namespace ShowWorkTests
{
    public class RegisterTest : BaseTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task BaseRegistrationTest()
        {
            using(TransactionScope scope = Helper.CreateTransactionsScope())
            {
                string login = Guid.NewGuid().ToString().Substring(0,10);

                //Validation: should not be in database
                var loginValidationResult = await authBL.ValidateLogin(login);
                Assert.IsNull(loginValidationResult);

                //Create user
                int userId = await authBL.CreateUser(
                    new ShowWork.DAL_MSSQL.Models.UserModel() 
                    { 
                        Login = login, 
                        Password = "Qwerty123!",
                        FirstName = Guid.NewGuid().ToString(),
                        SecondName = Guid.NewGuid().ToString(),
                        Email = Guid.NewGuid().ToString() + "@test.com",
                    });
                Assert.Greater(userId, 0);

                //Validation: should be in database
                loginValidationResult = await authBL.ValidateLogin(login);
                Assert.IsNotNull(loginValidationResult);
            }
            Assert.Pass();
        }
    }
}