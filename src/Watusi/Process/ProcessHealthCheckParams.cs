using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Watusi.HealthChecks
{
    public class ProcessHealthCheckParams:HealthCheckParams<ProcessHealthCheckParams, bool>
    {
        public ProcessHealthCheckParams(string processName,int? count, Action<string, HealthCheckResult<ProcessHealthCheckParams, bool>> notify):base(notify)
        {
            ProcessName = processName;
            Count = count;
        }
        public string ProcessName { get; set; }
        public int? Count { get; set; }
    }
}
