using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Watusi.HealthChecks
{
    public class DiskFreeSpaceHealthCheck : HealthCheck<DiskFreeSpaceHealthCheckParams, long>
    {
        
        private DiskFreeSpaceHealthCheckParams _params;
        public override string Name => $"DiskFreeSpace({_params.DriveName})";

        public DiskFreeSpaceHealthCheck(DiskFreeSpaceHealthCheckParams healthCheckParams) : base(healthCheckParams)
        {
            _params = healthCheckParams;
        }
        protected override HealthCheckStatus Check(long healthResult) => _params.DecideStatus(healthResult); 
        protected override async Task<long> Health()
        {
            var drive = DriveInfo.GetDrives().Single(q=>q.Name==_params.DriveName);

            var result = drive.AvailableFreeSpace / 1024 / 1024;

            return await Task.FromResult(result);
        }
    }
}
