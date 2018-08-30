using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Watusi.HealthChecks
{
    public class HttpHealthCheckParams:HealthCheckParams<HttpHealthCheckParams,bool>
    {
        public HttpHealthCheckParams(string url, Action<string, HealthCheckResult<HttpHealthCheckParams, bool>> notify):base(notify)
        {
            Url = url;
        }
        public string Url { get; set; }
    }
}
