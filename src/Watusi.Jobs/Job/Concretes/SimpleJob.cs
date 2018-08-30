using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watusi.Middlewares;

namespace Watusi
{
    public class SimpleJob : IJob
    {
        public event JobExceptionEventHandler RaiseJobExceptionEvent;

        protected ILogger _logger;
        public string Name { get; private set; }
        protected IJobContext _jobContext;
        private Func<IJobContext, Task> _middleware;
        private Policy _policy;

        public SimpleJob(ILogger logger,string name, IJobContext jobContext=null)
        {
            _logger = logger;
            Name = name ?? GetType().Name;
            _jobContext = jobContext;
        }

        public void Use(Func<IJobContext, Task> middleware, Policy policy = null)
        {
            _middleware = middleware;
            _policy = policy;
        }

        public virtual async Task Run()
        {
            _logger?.LogInformation("Job {jobName} started",Name);

            try
            {
                await RunMiddleware(_middleware, _policy);
            }
            catch (Exception ex)
            {
                OnRaiseJobExceptionEvent(new JobExceptionEventArgs(this, ex));
                throw; // re thrown exception
            }

            _logger?.LogInformation("Job {jobName} finished", Name);
        }

        protected async Task RunMiddleware(Func<IJobContext, Task> middleware,Policy policy)
        {
            if (policy!=null)
            {
                await policy.ExecuteAsync(async () => await middleware.Invoke(_jobContext));
            }
            else
            {
                await middleware.Invoke(_jobContext);
            }
        }

        protected virtual void OnRaiseJobExceptionEvent(JobExceptionEventArgs e)
        {
            JobExceptionEventHandler handler = RaiseJobExceptionEvent;
            handler?.Invoke(this,e);
        }
    }



    public delegate void JobExceptionEventHandler(object sender, JobExceptionEventArgs args);

    public class JobExceptionEventArgs : EventArgs
    {
        public JobExceptionEventArgs(IJob job, Exception ex)
        {
            Job = job;
            Exception = ex;
        }

        public IJob Job { get; private set; }

        public Exception Exception { get; private set; }
    }
}
