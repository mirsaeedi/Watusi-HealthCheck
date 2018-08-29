using System;
using System.Collections.Generic;
using System.Text;

namespace Watusi
{
    public abstract class JobDefinition
    {
        public void RunJob()
        {
            new JobRunner(null).RunJob(DefineJob);
        }
        protected abstract IJob DefineJob();
    }
}
