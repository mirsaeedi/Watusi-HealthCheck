using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;


namespace Watusi.Infrastructure.Network
{
    public class TelnetHealthCheck:HealthCheck<TelnetHealthCheckParams,bool>
    {
        private IPAddress _ipAddress;
        private string _dnsName;
        private int _port;

        private TelnetHealthCheckParams _params;

        public override string Name => $"Telnet({_ipAddress.ToString() ?? _dnsName}:{_port})";

        public TelnetHealthCheck(TelnetHealthCheckParams healthCheckParams) :base(healthCheckParams)
        {
            _params = healthCheckParams;

            _ipAddress = healthCheckParams.IpAddress;
            _dnsName= healthCheckParams.DnsName;
            _port = healthCheckParams.Port;
        }

        public override bool Check(bool healthResult) => healthResult;

        public async override Task<bool> Health()
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
