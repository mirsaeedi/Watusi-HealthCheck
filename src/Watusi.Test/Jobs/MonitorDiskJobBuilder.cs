using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Watusi.HealthChecks;

namespace Watusi.Samples.Jobs
{
    public class MonitorDiskJobBuilder : JobBuilder
    {
        public MonitorDiskJobBuilder(ILogger logger=null) : base(logger) {  }

        protected override IJob Build()
        {
            var job = new PipelineJob(Logger,"DiskJobs");
            job.RaiseJobExceptionEvent += Job_RaiseJobExceptionEvent;

            job.Use((_) =>
            {
                var diskFreeSpaceCheck = new DiskFreeSpaceHealthCheck(
                    new DiskFreeSpaceHealthCheckParams("C:"
                    , (fs) => fs < 10000 ? HealthCheckStatus.Healthy : HealthCheckStatus.Unhealthy
                    , (m, r) => Console.WriteLine(m)));

                return diskFreeSpaceCheck.Beat();
            })
            .Use((_) =>
            {
                var fileSystemHealthCheck = new FileSystemHealthCheck(
                    new FileSystemHealthCheckParams("C:\fileToCheck.txt"
                    , (exists) => exists ? HealthCheckStatus.Healthy : HealthCheckStatus.Unhealthy
                    , (m, r) => Console.WriteLine(m)));

                return fileSystemHealthCheck.Beat();
            });

            return job;
        }

        private void Job_RaiseJobExceptionEvent(object sender, JobExceptionEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
        }
    }
}
