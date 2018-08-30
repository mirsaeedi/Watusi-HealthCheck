using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Watusi.HealthChecks;

namespace Watusi.Samples.Jobs
{
    public class HttpJobBuilder : JobBuilder
    {
        protected override IJob Build()
        {
            var job = new SimpleJob(Logger,"HttpCheck");
            job.RaiseJobExceptionEvent += Job_RaiseJobExceptionEvent;

            var policy = Policy.Handle<HttpRequestException>().WaitAndRetry(new[]
                        {
                            TimeSpan.FromSeconds(1),
                            TimeSpan.FromSeconds(2),
                            TimeSpan.FromSeconds(3)
                        }); 

            job.Use((_) =>
            {
                var httpCheck = new HttpHealthCheck(new HttpHealthCheckParams("google.com", (m, r) => Console.WriteLine(m)));
                return httpCheck.Beat();
            }, policy);

            return job;
        }

        private void Job_RaiseJobExceptionEvent(object sender, JobExceptionEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
        }
    }
}
