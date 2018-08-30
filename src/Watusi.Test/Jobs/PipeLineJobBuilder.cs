using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Watusi.HealthChecks;

namespace Watusi.Samples.Jobs
{
    public class PipelineJobBuilder : JobBuilder
    {
        public PipelineJobBuilder(ILogger logger=null) : base(logger) {  }

        protected override IJob Build()
        {
            var job = new PipelineJob(Logger,"pipeline");
            job.RaiseJobExceptionEvent += Job_RaiseJobExceptionEvent;

            var jobContext = new JobContext();
            jobContext["param1"] = 1;

            job.Use((context) =>
            {
                var param1 = jobContext["param1"];
                jobContext["param2"]="Hello Next Middleware!";

                return Task.CompletedTask;
            })
            .Use((context) =>
            {
                var param2 = context["param2"];

                return Task.CompletedTask;
            });

            return job;
        }

        private void Job_RaiseJobExceptionEvent(object sender, JobExceptionEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
        }
    }
}
