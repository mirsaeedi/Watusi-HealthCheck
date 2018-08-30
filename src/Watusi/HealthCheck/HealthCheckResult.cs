using System;
using System.Collections.Generic;
using System.Text;

namespace Watusi.HealthChecks
{
    public class HealthCheckResult<THealthCheckParams,TResult>
    {
        public HealthCheckResult(HealthCheckStatus status, THealthCheckParams healthCheckParams, TResult healthResult)
        {
            Status = status;
            Params = healthCheckParams;
            Result= healthResult;
        }

        public HealthCheckStatus Status { get; private set; }
        public THealthCheckParams Params { get; private set; }
        public TResult Result { get; private set; }
    }

    public enum HealthCheckStatus
    {
        Healthy,
        Warning,
        Unhealthy
    }
}
