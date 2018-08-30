using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;


namespace Watusi.HealthChecks
{
    public class TelnetHealthCheck:HealthCheck<TelnetHealthCheckParams,bool>
    {
        private TelnetHealthCheckParams _params;

        public override string Name => $"Telnet({_params.IpAddress.ToString() ?? _params.DnsName}:{_params.Port})";

        public TelnetHealthCheck(TelnetHealthCheckParams healthCheckParams) :base(healthCheckParams)
        {
            _params = healthCheckParams;
        }

        protected override HealthCheckStatus Check(bool healthResult) => healthResult?HealthCheckStatus.Healthy:HealthCheckStatus.Unhealthy;

        protected async override Task<bool> Health()
        {
            var isAvailable = false;

            for (int i = 0; i < _params.RetryCount; i++)
            {
                isAvailable = await Telnet();

                if (isAvailable)
                    break;
            }

            return isAvailable;
        }

        private async Task<bool> Telnet()
        {
            var telnetWasSuccessfull = false;

            var timeout = 5 * 1000;

            using (var client = new TcpClient())
            {
                try
                {
                    client.ReceiveTimeout = timeout;
                    client.SendTimeout = timeout;

                    if (_params.DnsName == null)
                        await client.ConnectAsync(_params.IpAddress, _params.Port);
                    else
                        await client.ConnectAsync(_params.DnsName, _params.Port);

                    telnetWasSuccessfull = client.Connected;
                }
                catch
                {
                    telnetWasSuccessfull = false;
                }
            }

            return telnetWasSuccessfull;
        }
    }
}
