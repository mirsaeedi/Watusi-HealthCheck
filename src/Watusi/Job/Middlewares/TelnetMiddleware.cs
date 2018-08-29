using System.Threading.Tasks;
using Watusi.Infrastructure.Network;

namespace Watusi.Middlewares
{
    public class TelnetMiddleware : IMiddleware
    {
        private TelnetHealthCheck _telnetHealthCheck;
        public TelnetMiddleware(TelnetHealthCheckParams healthCheckParams)
        {
            _telnetHealthCheck = new TelnetHealthCheck(healthCheckParams);
        }
        public async Task Run(IJobContext jobContext)
        {
            await _telnetHealthCheck.Beat();
        }
    }
}
