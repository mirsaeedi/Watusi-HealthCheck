using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Watusi.HealthChecks
{
    public class HttpHealthCheck : HealthCheck<HttpHealthCheckParams, bool>
    {
        private HttpHealthCheckParams _params;
        public override string Name => $"Http({_params.Url})";

        public HttpHealthCheck(HttpHealthCheckParams healthCheckParams) : base(healthCheckParams)
        {
            _params = healthCheckParams;
        }
        protected override HealthCheckStatus Check(bool healthResult) => healthResult ? HealthCheckStatus.Healthy : HealthCheckStatus.Unhealthy;
        protected override async Task<bool> Health()
        {
            var response = await GetHttpClinet().GetAsync(_params.Url);
            return response.IsSuccessStatusCode;
        }

        private HttpClient GetHttpClinet() =>  new HttpClient();
    }
}
