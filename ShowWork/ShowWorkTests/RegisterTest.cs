using ShowWork.BL;
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
                await authBL.ValidateLogin(login);

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
                var user = await authDal.GetUser(userId);
                Assert.That(login, Is.EqualTo(user.Login));
                Assert.NotNull(user.Salt);

                var userByLogin = await authDal.GetUser(login);
                Assert.That(login, Is.EqualTo(user.Login));

                //Validation: should be in database
                Assert.Throws<DuplicateLoginException>(delegate { authBL.ValidateLogin(login).GetAwaiter().GetResult();}) ;

                string encpas = encrypt.HashPassword("Qwerty123!", userByLogin.Salt);
                Assert.That(encpas, Is.EqualTo(user.Password));

                
                
            }
            Assert.Pass();
        }
    }
}