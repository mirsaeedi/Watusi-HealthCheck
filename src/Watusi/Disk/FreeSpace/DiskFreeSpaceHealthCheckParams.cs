using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Watusi.HealthChecks
{
    public class DiskFreeSpaceHealthCheckParams : HealthCheckParams<DiskFreeSpaceHealthCheckParams, long>
    {

        public DiskFreeSpaceHealthCheckParams(string driveName, Func<long, HealthCheckStatus> decideStatus
            , Action<string, HealthCheckResult<DiskFreeSpaceHealthCheckParams, long>> notify):base(notify)
        {
            DriveName = driveName;
            DecideStatus = decideStatus;
        }
        public string DriveName { get; set; }

        public Func<long,HealthCheckStatus> DecideStatus { get; set; }
    }
}
