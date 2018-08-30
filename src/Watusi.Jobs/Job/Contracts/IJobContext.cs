using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watusi
{
    public interface IJobContext
    {
        bool ContinueChain { get; set; }
        object this[string index]
        {
            get;
            set;
        }
        dynamic Settings { get; }

    }
}
