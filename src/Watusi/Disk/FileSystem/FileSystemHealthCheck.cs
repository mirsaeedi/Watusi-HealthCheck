using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Watusi.HealthChecks
{
    public class FileSystemHealthCheck : HealthCheck<FileSystemHealthCheckParams, bool>
    {
        
        private FileSystemHealthCheckParams _params;
        public override string Name => $"FileSystem({_params.Path})";

        public FileSystemHealthCheck(FileSystemHealthCheckParams healthCheckParams) : base(healthCheckParams)
        {
            _params = healthCheckParams;
        }
        protected override HealthCheckStatus Check(bool healthResult) => _params.DecideStatus(healthResult); 
        protected override async Task<bool> Health()
        {
            var result = File.Exists(_params.Path) || Directory.Exists(_params.Path);
            return await Task.FromResult(result);
        }
    }
}
