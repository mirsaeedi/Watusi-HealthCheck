using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Watusi.Infrastructure.Network
{
    public class TelnetHealthCheckParams:HealthCheckParams<TelnetHealthCheckParams,bool>
    {
        public IPAddress IpAddress { get; set; }
        public string DnsName { get; set; }
        public int Port { get; set; }
        public int RetryCount { get; set; } = 3;
    }
}
