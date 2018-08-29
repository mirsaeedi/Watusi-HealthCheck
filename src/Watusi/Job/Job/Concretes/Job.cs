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
    public class Job : IJob
    {
        public event JobExceptionEventHandler RaiseJobExceptionEvent;

        private ILogger _logger;

        public string Name { get; set; }

        private List<IMiddleware> _middlewares = new List<IMiddleware>();
        private IJobContext _jobContext;
        private int _loopCount;
        private TimeSpan _timeSpan;
        //private OpenDatabaseTransactionMiddleware _openDatabaseTransactionMiddleware;
        private Action<int, IJobContext> _jobContextInitializer;

        public Job(ILogger logger)
        {
            _logger = logger;
            Name = this.GetType().Name;
        }

        public Job(IJobContext jobContext,string name)
        {
            Name = name??this.GetType().Name;
            _jobContext = jobContext;
            _loopCount = 0;
            _timeSpan = new TimeSpan(0, 0, 0);
        }
        

        public Job Use(IMiddleware middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }

        public Job Loop(int repeatCount,Action<int,IJobContext> jobContextInitializer)
        {
            _jobContextInitializer = jobContextInitializer;
            _loopCount = repeatCount;
            return this;
        }

        /*public Job OpenDatabaseTransaction()
        {
            var openDatabaseTransactionMiddleware = new OpenDatabaseTransactionMiddleware();
            _middlewares.Add(openDatabaseTransactionMiddleware);
            return this;
        }

        public Job CloseDatabaseTransaction()
        {
            var closeDatabaseTransactionMiddleware = new CloseDatabaseTransactionMiddleware();
            _middlewares.Add(closeDatabaseTransactionMiddleware);
            return this;
        }*/

        public Job Loop(TimeSpan timeInterval)
        {
            _timeSpan = timeInterval;
            return this;
        }

        public virtual void Run()
        {
            _logger?.LogInformation("Job {jobName} started",this.Name);

            var endTime = DateTime.Now.Add(_timeSpan);
            var loop = 0;


            do
            {
                _jobContextInitializer?.Invoke(loop, _jobContext);
                RunPipeline();
                loop++;

            } while (loop < _loopCount || DateTime.Now < endTime);

            _logger?.LogInformation("Job {jobName} finished", this.Name);
        }

        private void RunPipeline()
        {

            try
            {
                foreach (var middleware in _middlewares)
                {
                    if (!_jobContext.ContinueChain)
                        return;

                    _logger?.LogInformation("Job {jobName} executes middleware {middlewareName}", this.Name,middleware.GetType().Name);

                    RunMiddleware(middleware);

                    _logger?.LogInformation("Job {jobName} executed middleware {middlewareName}", this.Name, middleware.GetType().Name);
                }
            }
            catch (Exception ex)
            {
                OnRaiseJobExceptionEvent(new JobExceptionEventArgs(this,ex));
                throw; // re thrown exception
            }
            
        }

        private void RunMiddleware(IMiddleware middleware)
        {
            if (IsRetryable(middleware))
            {
                var policy = GetRetryPolicy(middleware);
                policy.Execute(() => middleware.Run(_jobContext));
            }
            else
            {
                middleware.Run(_jobContext);
            }
        }

        private bool IsRetryable(IMiddleware middleware)
        {
            var middlewareTypes = middleware.GetType();
            var isRetryable = middlewareTypes.GetCustomAttributes(typeof(RetryAttribute), false).Count() == 1;
            return isRetryable;
        }

        private RetryPolicy GetRetryPolicy(IMiddleware middleware)
        {
            return Policy
                            .Handle<Exception>()
                            .WaitAndRetry(new[]
                            {
                                TimeSpan.FromSeconds(40),
                                TimeSpan.FromSeconds(60),
                                TimeSpan.FromSeconds(100)
                            }, (exception, timeSpan) =>
                            {
                                _logger?.LogInformation("Job {jobName} failed to execute middleware {middlewareName}, retried after {timespan}", this.Name, middleware.GetType().Name,timeSpan);
                            });
        }

        protected virtual void OnRaiseJobExceptionEvent(JobExceptionEventArgs e)
        {
            JobExceptionEventHandler handler = RaiseJobExceptionEvent;
            handler?.Invoke(this,e);
        }
    }
}
