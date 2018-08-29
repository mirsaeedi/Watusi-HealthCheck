using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Watusi;
using Watusi.Infrastructure.Network;
using Watusi.Middlewares;

namespace Watusi.Test.Jobs
{
    public class TelnetJob : JobDefinition
    {
        private static Lazy<TelnetMiddleware> telnetGoogle = new Lazy<TelnetMiddleware>
            (() => new TelnetMiddleware(new TelnetHealthCheckParams()
            {
                RetryCount = 3,
                DnsName = "google.com",
                Port = 80,
                Notify = (msg, healthCheckParams, result) => Console.WriteLine(result),
                GreenMessage = "",
                RedMessage = "",
            }));

        private static Lazy<TelnetMiddleware> telnetYahoo = new Lazy<TelnetMiddleware>
            (() => new TelnetMiddleware(new TelnetHealthCheckParams()
            {
                RetryCount = 3,
                DnsName = "yahoo.com",
                Port = 80,
                Notify = (msg, healthCheckParams, result) => Console.WriteLine(result),
                GreenMessage = "",
                RedMessage = "",
            }));

        protected override IJob DefineJob()
        {
            var jobContext = new JobContext();
            var job = new Job(jobContext, GetType().Name);

            job.Use(telnetGoogle.Value);
            job.Use(telnetYahoo.Value);

            return job;
        }
    }
}
