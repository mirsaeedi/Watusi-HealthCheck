using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Watusi.HealthChecks
{
    public class HttpHealthCheckParams:HealthCheckParams<HttpHealthCheckParams,bool>
    {
        public string Url { get; set; }
    }
}
