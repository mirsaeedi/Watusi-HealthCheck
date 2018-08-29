using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Watusi
{
    public class JobRunner
    {
        private  object _lock = new object();
        private  Dictionary<string, object> _internalLockDic = new Dictionary<string, object>();
        private ILogger _logger;

        public JobRunner(ILogger logger)
        {
            _logger = logger;
        }

        public void RunJob(Func<IJob> jobCreator)
        {   
            var job = jobCreator.Invoke();
            job.RaiseJobExceptionEvent += HandleJobExceptionEvent;

            
            var lockObject = GetLock(job.Name);
            _logger.LogInformation("Trying to Aquire Lock For job {JobName}, with Lock Id {lockId}"
                , job.Name,lockObject.GetHashCode());

            lock (lockObject)
            {
                _logger.LogInformation("Aquire Lock For job {JobName}, with Lock Id {lockId}"
                , job.Name, lockObject.GetHashCode());
                job.Run();
            }

            _logger.LogInformation("Release Lock For job {JobName}, with Lock Id {lockId}"
                , job.Name, lockObject.GetHashCode());
        }

        public void RunJob(string jobName)
        {
            var job = DefineJob(jobName);
            job.RaiseJobExceptionEvent += HandleJobExceptionEvent;

            var lockObject = GetLock(jobName);
            _logger.LogInformation("Trying to Aquire Lock For job {JobName}, with Lock Id {lockId}"
                , job.Name, lockObject.GetHashCode());

            lock (lockObject)
            {
                _logger.LogInformation("Aquire Lock For job {JobName}, with Lock Id {lockId}"
                , job.Name, lockObject.GetHashCode());

                job.Run();
            }

            _logger.LogInformation("Release Lock For job {JobName}, with Lock Id {lockId}"
                , job.Name, lockObject.GetHashCode());
        }

        private static IJob DefineJob(string jobName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                var jobTypes = assembly.GetTypes()
                .AsParallel()
                .Where(t => t.GetCustomAttributes(typeof(JobAttribute), false).Count() == 1)
                .Select(t =>
                new
                {
                    JobType = t,
                    AttributeValue = t.GetCustomAttributes(typeof(JobAttribute), false).FirstOrDefault() as JobAttribute
                }).ToArray();

                var jobType = jobTypes.SingleOrDefault(q => q.AttributeValue.Name == jobName);

                if (jobType != null)
                {
                    var job = Activator.CreateInstance(jobType.JobType) as IJob;
                    return job;
                }
            }

            throw new Exception($"Can not find job with name {jobName}");
        }

        private void HandleJobExceptionEvent(object sender, JobExceptionEventArgs args)
        {
            
        }

        private object GetLock(string jobName)
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