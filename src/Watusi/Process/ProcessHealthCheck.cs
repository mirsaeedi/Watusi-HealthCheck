using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;


namespace Watusi.HealthChecks
{
    public class ProcessHealthCheck : HealthCheck<ProcessHealthCheckParams,bool>
    {
        private ProcessHealthCheckParams _params;

        public override string Name => $"Process({_params.ProcessName})";

        public ProcessHealthCheck(ProcessHealthCheckParams healthCheckParams) :base(healthCheckParams)
        {
            _params = healthCheckParams;
        }

        protected override HealthCheckStatus Check(bool healthResult) => healthResult?HealthCheckStatus.Healthy:HealthCheckStatus.Unhealthy;

        protected async override Task<bool> Health()
        {
            var processes = Process.GetProcessesByName(_params.ProcessName);

            var result = !_params.Count.HasValue & processes.Length>0 ? true
                : processes.Length == _params.Count?true:false;

            return await Task.FromResult(result);
        }
    }
}
