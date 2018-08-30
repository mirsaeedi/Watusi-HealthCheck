using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Watusi
{
    public abstract class JobBuilder
    {
        private ILogger _logger;
        public JobBuilder(ILogger logger)
        {
            _logger = logger;
        }
        public void Run()
        {
            JobRuntimEnvironment.RunJob(Build(), _logger);
        }
        protected abstract IJob Build();
    }
}
