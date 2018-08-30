using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Watusi.HealthChecks
{
    public class FileSystemHealthCheckParams : HealthCheckParams<FileSystemHealthCheckParams, bool>
    {
        public FileSystemHealthCheckParams(string path, Func<bool, HealthCheckStatus> decideStatus
            , Action<string, HealthCheckResult<FileSystemHealthCheckParams, bool>> notify):base(notify)
        {
            Path = path;
            DecideStatus = decideStatus;
        }
        public string Path { get; set; }

        public Func<bool, HealthCheckStatus> DecideStatus { get; set; }
    }
}
