using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Watusi.HealthChecks
{
    public class TelnetHealthCheckParams:HealthCheckParams<TelnetHealthCheckParams, bool>
    {
        public TelnetHealthCheckParams(IPAddress ipAddress, string dnsName, int port,int retryCount
            , Action<string, HealthCheckResult<TelnetHealthCheckParams, bool>> notify):base(notify)
        {
            Port = port;
            IPAddress = ipAddress;
            DnsName = dnsName;
            RetryCount = retryCount;
        }
        public IPAddress IPAddress { get; set; }
        public string DnsName { get; set; }
        public int Port { get; set; }
        public int RetryCount { get; set; }
    }
}
