/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Watusi.Middlewares
{
    public class OpenDatabaseTransactionMiddleware : IMiddleware
    {
        public OpenDatabaseTransactionMiddleware()
        {

        }

        public void Run(IJobContext jobContext)
        {
            var scope = new TransactionScope(TransactionScopeOption.Required, new System.TimeSpan(1, 0, 0));
            jobContext["JobContextTransactionScopeFeature"] = scope;
        }
    }
}
*/