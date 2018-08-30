using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Watusi.HealthChecks
{
    public class DbHealthCheckParams<TReturn>:HealthCheckParams<DbHealthCheckParams<TReturn>, TReturn>
    {
        public string ConnectionString { get; set; }
        public string Query { get; set; }
        public Func<TReturn,HealthCheckStatus> DecideStatus { get; set; } 
    }
}
