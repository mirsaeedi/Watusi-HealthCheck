using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watusi
{
    public interface IJob
    {   
        event JobExceptionEventHandler RaiseJobExceptionEvent;

        void Run();

        string Name { get; set; }
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
