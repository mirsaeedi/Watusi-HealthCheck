using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Watusi
{
    public abstract class JobBuilder
    {
        protected ILogger Logger  { get; private set; }
        public JobBuilder(ILogger logger=null)
        {
            Logger = logger;
        }
        public void Run()
        {
            JobRuntimEnvironment.RunJob(Build(), Logger);
        }
        protected abstract IJob Build();
    }
}
