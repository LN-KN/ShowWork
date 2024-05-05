using ShowWorkTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ShowWorkTests
{
    public class TokenTest : Helpers.BaseTest
    {
        [Test]
        public async Task BaseTokenTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionsScope())
            {
                var tokenId = await this.userTokenDAL.Create(10);
                var userid = await this.userTokenDAL.Get(tokenId);
                Assert.That(userid, Is.EqualTo(10));
            }
        }
    }
}
