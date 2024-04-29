using ShowWork.BL;
using ShowWork.BL.Auth;
using ShowWorkTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ShowWorkTests
{
    public class AuthTest : BaseTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task AuthRegistrationTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionsScope())
            {
                string login = Guid.NewGuid().ToString().Substring(0, 10);

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

                //Validation: should be in database
                Assert.Throws<DuplicateLoginException>(delegate { authBL.ValidateLogin(login).GetAwaiter().GetResult(); });

                Assert.Throws<AuthorizationException>(() => authBL.Authenticate("asdasdf", "asdfas", false).GetAwaiter().GetResult());
                Assert.Throws<AuthorizationException>(() => authBL.Authenticate(login, "asd1", false).GetAwaiter().GetResult());
                Assert.Throws<AuthorizationException>(() => authBL.Authenticate("afasdf", "Qwerty123!", false).GetAwaiter().GetResult());
                await authBL.Authenticate(login, "Qwerty123!", false);

                string? authCookie = this.webCookie.Get(AuthConstants.SessionCookieName);
                Assert.NotNull(authCookie);

                string? rememberMeCookie = this.webCookie.Get(AuthConstants.RememberMeCookieName);
                Assert.Null(rememberMeCookie);
            }
            Assert.Pass();
        }
        public async Task RememberMeTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionsScope())
            {
                string login = Guid.NewGuid().ToString();

                // create user
                int userId = await authBL.CreateUser(
                   new ShowWork.DAL_MSSQL.Models.UserModel()
                   {
                       Login = login,
                       Password = "Qwerty123!",
                       FirstName = Guid.NewGuid().ToString(),
                       SecondName = Guid.NewGuid().ToString(),
                       Email = Guid.NewGuid().ToString() + "@test.com",
                   });

                await authBL.Authenticate(login, "qwer1234", true);

                string? authCookie = this.webCookie.Get(AuthConstants.SessionCookieName);
                Assert.NotNull(authCookie);

                string? rememberMeCookie = this.webCookie.Get(AuthConstants.RememberMeCookieName);
                Assert.NotNull(rememberMeCookie);
            }
        }
    }
}
