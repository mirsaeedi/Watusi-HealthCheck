using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Watusi.HealthChecks
{
    public class DiskHealthCheckParams : HealthCheckParams<DiskHealthCheckParams, long>
    {
        public string DriveName { get; set; }

        public Func<long,HealthCheckStatus> DecideStatus { get; set; }
    }
}
