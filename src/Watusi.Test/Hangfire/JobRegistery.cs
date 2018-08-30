using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Watusi.Samples.Jobs;

namespace Watusi.Samples.Hangfire
{
    public static class JobRegistery
    {
        public static void Register()
        {
            RecurringJob.AddOrUpdate<MonitorDiskJobBuilder>((job) => job.Run(), Cron.Minutely);
            RecurringJob.AddOrUpdate<HttpJobBuilder>((job) => job.Run(), Cron.Minutely);
            RecurringJob.AddOrUpdate<PipelineJobBuilder>((job) => job.Run(), Cron.Minutely);
            RecurringJob.AddOrUpdate<SqlServerJobBuilder>((job) => job.Run(), Cron.Minutely);
        }
        
    }
}
