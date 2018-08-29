using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Watusi.Infrastructure
{
    public abstract class HealthCheck<THealthCheckParams,TResult> where THealthCheckParams: HealthCheckParams<THealthCheckParams, TResult>
    {
        public abstract string Name { get; }
        private THealthCheckParams _healthCheckParams;

        public HealthCheck(THealthCheckParams healthCheckParams)
        {
            _healthCheckParams = healthCheckParams;
        }
        public async Task<bool> Beat()
        {
            var healthResult = await Health();
            var newState =  Check(healthResult);

            if (newState)
            {
                NotifyOnGreenStatus(_healthCheckParams, healthResult);
            }
            else
            {
                NotifyOnRedStatus(_healthCheckParams, healthResult);
            }

            return newState;

        }
        public virtual void NotifyOnRedStatus(THealthCheckParams healthCheckParams ,TResult healthResult)
        {
            var message = _healthCheckParams.RedMessage
                .Replace("{name}", Name)
                .Replace("{result}",healthResult.ToString());

            _healthCheckParams.Notify(message,healthCheckParams,healthResult);

        }

        public virtual void NotifyOnGreenStatus(THealthCheckParams healthCheckParams, TResult healthResult)
        {
            var message = _healthCheckParams.GreenMessage
                .Replace("{name}", Name)
                .Replace("{result}", healthResult.ToString());

            _healthCheckParams.Notify(message, healthCheckParams, healthResult);
        }

        public abstract Task<TResult> Health();
        public abstract bool Check(TResult healthResult);
    }
}
