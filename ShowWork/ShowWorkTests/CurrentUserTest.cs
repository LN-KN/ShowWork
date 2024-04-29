using ShowWork.BL.Auth;
using ShowWork.DAL_MSSQL.Models;
using ShowWorkTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ShowWorkTests
{
    public class CurrentUserTest : Helpers.BaseTest
    {
        [Test]
        public async Task BaseRegistrationTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionsScope())
            {
                await CreateAndAuthUser();

                bool isLoggedIn = await this.currentUser.IsLoggedIn();
                Assert.True(isLoggedIn);

                webCookie.Delete(AuthConstants.SessionCookieName);
                dbSession.ResetSessionCache();

                isLoggedIn = await this.currentUser.IsLoggedIn();
                Assert.True(isLoggedIn);

                webCookie.Delete(AuthConstants.SessionCookieName);
                webCookie.Delete(AuthConstants.RememberMeCookieName);
                dbSession.ResetSessionCache();

                isLoggedIn = await this.currentUser.IsLoggedIn();
                Assert.False(isLoggedIn);
            }
        }

        public async Task<int> CreateAndAuthUser()
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
            return await authBL.Authenticate(login, "Qwerty123!", true);
        }
    }
}
