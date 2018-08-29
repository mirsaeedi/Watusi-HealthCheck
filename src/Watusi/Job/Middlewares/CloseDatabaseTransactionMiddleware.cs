/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Watusi.Middlewares
{
    public class CloseDatabaseTransactionMiddleware : IMiddleware
    {
        public CloseDatabaseTransactionMiddleware()
        {
        }

        public void Run(IJobContext jobContext)
        {
            var scope = jobContext["JobContextTransactionScopeFeature"] as TransactionScope;
            scope.Complete();
            scope.Dispose();
        }
    }
}
*/