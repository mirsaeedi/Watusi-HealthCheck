using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Watusi.HealthChecks
{
    public class ProcessHealthCheckParams:HealthCheckParams<ProcessHealthCheckParams, bool>
    {
        public string ProcessName { get; set; }
        public int? Count { get; set; }
    }
}
