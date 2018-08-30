using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Watusi.HealthChecks
{
    public class DbHealthCheckParams<TReturn>:HealthCheckParams<DbHealthCheckParams<TReturn>, TReturn>
    {
        public DbHealthCheckParams(string connectionString, string query,
            Func<TReturn, HealthCheckStatus> decideStatus, Action<string, HealthCheckResult<DbHealthCheckParams<TReturn>, TReturn>> notify):base(notify)
        {
            connectionString = ConnectionString;
            Query = query;
            DecideStatus = decideStatus;
        }
        public string ConnectionString { get; set; }
        public string Query { get; set; }
        public Func<TReturn,HealthCheckStatus> DecideStatus { get; set; } 
    }
}
