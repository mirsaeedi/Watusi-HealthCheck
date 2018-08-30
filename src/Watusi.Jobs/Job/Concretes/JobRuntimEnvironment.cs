using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Watusi
{
    public static class JobRuntimEnvironment
    {
        private static object _lock = new object();

        private static Dictionary<string, object> _internalLockDic = new Dictionary<string, object>();

        public static void RunJob(IJob job,ILogger logger)
        {   
            var lockObject = GetLock(job.Name);
            logger?.LogInformation("Aquiring lock for job {JobName}, with lock id {lockId}", job.Name,lockObject.GetHashCode());

            lock (lockObject)
            {
                logger?.LogInformation("Lock is aquired for job {JobName}, with lock id {lockId}", job.Name, lockObject.GetHashCode());
                job.Run();
            }

            logger?.LogInformation("Lock is released for job {JobName}, with lock id {lockId}", job.Name, lockObject.GetHashCode());
        }
        private static object GetLock(string jobName)
        {
            lock (_lock)
            {
                if (!_internalLockDic.ContainsKey(jobName))
                    _internalLockDic[jobName] = new object();

                return _internalLockDic[jobName];
            }
        }
    }
}