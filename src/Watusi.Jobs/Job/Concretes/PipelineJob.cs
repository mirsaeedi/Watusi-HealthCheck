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
    public class PipelineJob : SimpleJob
    {
        private Action<int, IJobContext> _jobContextInitializer;
        private List<(Func<IJobContext, Task>,Policy)> _middlewares = new List<(Func<IJobContext, Task>, Policy)>();
        private int _loopCount;
        private TimeSpan _timeSpan;

        public PipelineJob(ILogger logger, string name, IJobContext jobContext = null) :base(logger,name, jobContext)
        {
            _loopCount = 0;
            _timeSpan = new TimeSpan(0, 0, 0);
        }
        
        public new PipelineJob Use(Func<IJobContext, Task> middleware,Policy policy=null)
        {
            _middlewares.Add((middleware,policy));
            return this;
        }

        public PipelineJob Loop(int repeatCount,Action<int,IJobContext> jobContextInitializer)
        {
            _jobContextInitializer = jobContextInitializer;
            _loopCount = repeatCount;
            return this;
        }

        public PipelineJob Loop(TimeSpan timeInterval)
        {
            _timeSpan = timeInterval;
            return this;
        }

        public override async Task Run()
        {
            _logger?.LogInformation("Job {jobName} started",this.Name);

            var endTime = DateTime.Now.Add(_timeSpan);
            var loop = 0;

            do
            {
                _jobContextInitializer?.Invoke(loop, _jobContext);
                await 
                    RunPipeline();
                loop++;

            } while (loop < _loopCount || DateTime.Now < endTime);

            _logger?.LogInformation("Job {jobName} finished", this.Name);
        }

        private async Task RunPipeline()
        {
            try
            {
                foreach (var (middleware, policy) in _middlewares)
                {
                    if (!_jobContext.ContinueChain)
                        return;

                    _logger?.LogInformation("Job {jobName} executes middleware {middlewareName}", Name,middleware.GetType().Name);

                    await RunMiddleware(middleware,policy);

                    _logger?.LogInformation("Job {jobName} executed middleware {middlewareName}", Name, middleware.GetType().Name);
                }
            }
            catch (Exception ex)
            {
                OnRaiseJobExceptionEvent(new JobExceptionEventArgs(this,ex));
                throw; // re thrown exception
            }
            
        }
    }
}
