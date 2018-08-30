using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Watusi.HealthChecks;

namespace Watusi.Samples.Jobs
{
    public class SqlServerJobBuilder : JobBuilder
    {
        protected override IJob Build()
        {
            var job = new PipelineJob(Logger, "SqlServerCheck");
            job.RaiseJobExceptionEvent += Job_RaiseJobExceptionEvent;
            var dbServerIP = IPAddress.Parse("1.1.1.1");

            var policy = Policy.Handle<HttpRequestException>().WaitAndRetry(new[]
                        {
                            TimeSpan.FromSeconds(1),
                            TimeSpan.FromSeconds(2),
                            TimeSpan.FromSeconds(3)
                        });

            job.Use((_) =>
            {
                var sqlQueryCheck = new SqlServerHealthCheck<int>(new DbHealthCheckParams<int>(
                    connectionString: "google.com"
                    ,query : "SELECT 1"
                    ,decideStatus : r=>r==1?HealthCheckStatus.Healthy:HealthCheckStatus.Unhealthy
                    ,notify: (m, r) => Console.WriteLine(m)));
                return sqlQueryCheck.Beat();
            }, policy)
            .Use((_) =>
            {
            var telnetCheck = new TelnetHealthCheck(new TelnetHealthCheckParams(
                    ipAddress: dbServerIP
                    , dnsName : null
                    ,port: 1433
                    ,retryCount:3
                    , notify: (m, r) => Console.WriteLine(m)));
                return telnetCheck.Beat();
            }, policy);

            return job;
        }

        private void Job_RaiseJobExceptionEvent(object sender, JobExceptionEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
        }
    }
}
