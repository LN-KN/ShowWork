using ShowWorkTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ShowWorkTests
{
    public class SessionTest : Helpers.BaseTest
    {
        [Test]
        [NonParallelizable]
        public async Task CreateSessionTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionsScope())
            {
                ((TestCookie)this.webCookie).Clear();
                this.dbSession.ResetSessionCache();
                var session = await this.dbSession.GetSession();

                var dbSessoion = await this.dbSessionDAL.Get(session.DbSessionId);

                Assert.NotNull(dbSessoion, "Session shoule not be null");

                Assert.That(dbSessoion.DbSessionId, Is.EqualTo(session.DbSessionId));

                var session2 = await this.dbSession.GetSession();
                Assert.That(dbSessoion.DbSessionId, Is.EqualTo(session2.DbSessionId));
            }
        }

        [Test]
        [NonParallelizable]
        public async Task CreateAuthorizedSessionTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionsScope())
            {
                ((TestCookie)this.webCookie).Clear();
                this.dbSession.ResetSessionCache();
                var session = await this.dbSession.GetSession();
                await this.dbSession.SetUserId(10);

                var dbSessoion = await this.dbSessionDAL.Get(session.DbSessionId);

                Assert.NotNull(dbSessoion, "Session shoule not be null");
                Assert.That(dbSessoion.UserId!, Is.EqualTo(10));

                Assert.That(dbSessoion.DbSessionId, Is.EqualTo(session.DbSessionId));

                var session2 = await this.dbSession.GetSession();
                Assert.That(dbSessoion.DbSessionId, Is.EqualTo(session2.DbSessionId));

                int? userid = await this.currentUser.GetCurrentUserId();
                Assert.That(userid, Is.EqualTo(10));
            }
        }
    }
}
