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
        public async Task CreateSessionTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionsScope())
            {
                var session = await this.dbSession.GetSession();

                var dbSessoion = await this.dbSessionDAL.Get(session.DbSessionId);

                Assert.NotNull(dbSessoion);

                Assert.That(dbSessoion.DbSessionId, Is.EqualTo(session.DbSessionId));

                var session2 = await this.dbSession.GetSession();
                Assert.That(dbSessoion.DbSessionId, Is.EqualTo(session2.DbSessionId));
            }
        }
    }
}
