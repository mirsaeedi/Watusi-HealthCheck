using System;
using System.Collections.Generic;
using System.Text;

namespace Watusi.HealthChecks
{
    public class HealthCheckParams<THealthCheckParams,TResult> 
    {
        public HealthCheckParams(Action<string, HealthCheckResult<THealthCheckParams, TResult>> notify)
        {
            Notify = notify;
        }

        public Action<string, HealthCheckResult<THealthCheckParams,TResult>> Notify { get; set; }
        public string HealthyMessageTemplate { get; set; } = "HealthCheck: {name}, Result={result},State={status}";
        public string UnHealthyMessageTemplate { get; set; } = "HealthCheck: {name}, Result={result},State={status}";
        public string WarningMessageTemplate { get; set; } = "HealthCheck: {name}, Result={result},State={status}";
    }
}
