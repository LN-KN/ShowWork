using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ShowWorkTests.Helpers
{
    internal class Helper : Helpers.BaseTest
    {
        public static TransactionScope CreateTransactionsScope(int seconds = 1)
        {
            return new TransactionScope(
                TransactionScopeOption.Required, 
                new TimeSpan(0,0, seconds), 
                TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
