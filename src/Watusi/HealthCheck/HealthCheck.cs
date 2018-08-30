using System.Threading.Tasks;

namespace Watusi.HealthChecks
{
    public abstract class HealthCheck<THealthCheckParams,TResult> where THealthCheckParams: HealthCheckParams<THealthCheckParams, TResult>
    {
        public abstract string Name { get; }
        private THealthCheckParams _healthCheckParams;

        public HealthCheck(THealthCheckParams healthCheckParams)
        {
            _healthCheckParams = healthCheckParams;
        }
        public async Task<HealthCheckResult<THealthCheckParams, TResult>> Beat()
        {
            var healthResult = await Health();
            var newStatus = Check(healthResult);
            var result = new HealthCheckResult<THealthCheckParams, TResult>(newStatus, _healthCheckParams, healthResult);

            Notify(result);

            return result;
        }

        private void Notify(HealthCheckResult<THealthCheckParams, TResult> result)
        {
            HealthCheckStatus newStatus = result.Status;

            if (newStatus == HealthCheckStatus.Healthy)
            {
                NotifyOnHealthyStatus(result);
            }
            else if (newStatus == HealthCheckStatus.Unhealthy)
            {
                NotifyOnUnHealthyStatus(result);
            }
            else
            {
                NotifyOnWarningStatus(result);
            }
        }

        protected virtual void NotifyOnWarningStatus(HealthCheckResult<THealthCheckParams, TResult> healthCheckResult)
        {
            var message = _healthCheckParams.WarningMessageTemplate
                            .Replace("{name}", Name)
                            .Replace("{result}", healthCheckResult.Result.ToString());

            _healthCheckParams.Notify(message, healthCheckResult);
        }

        protected virtual void NotifyOnUnHealthyStatus(HealthCheckResult<THealthCheckParams, TResult> healthCheckResult)
        {
            var message = _healthCheckParams.UnHealthyMessageTemplate
                .Replace("{name}", Name)
                .Replace("{result}", healthCheckResult.Result.ToString());

            _healthCheckParams.Notify(message, healthCheckResult);
        }
        protected virtual void NotifyOnHealthyStatus(HealthCheckResult<THealthCheckParams, TResult> healthCheckResult)
        {
            var message = _healthCheckParams.HealthyMessageTemplate
                .Replace("{name}", Name)
                .Replace("{result}", healthCheckResult.Result.ToString());

            _healthCheckParams.Notify(message, healthCheckResult);
        }

        protected abstract Task<TResult> Health();
        protected abstract HealthCheckStatus Check(TResult healthResult);
    }
}
