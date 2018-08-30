using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Watusi.HealthChecks
{
    public class SqlServerHealthCheck<TResult> : DbHealthCheck<TResult>
    {
        public SqlServerHealthCheck(DbHealthCheckParams<TResult> healthCheckParams) : base(healthCheckParams)
        {
            
        }
        protected override DbConnection CreateConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
