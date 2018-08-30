using System;
using System.Collections.Generic;
using System.Text;

namespace Watusi.HealthChecks
{
    public class HealthCheckParams<THealthCheckParams,TResult> 
    {
        public Action<string, HealthCheckResult<THealthCheckParams,TResult>> Notify { get; set; }
        public string HealthyMessageTemplate { get; set; }
        public string UnHealthyMessageTemplate { get; set; }
        public string WarningMessageTemplate { get; set; }
    }
}
