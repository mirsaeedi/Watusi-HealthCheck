using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watusi
{
    public class JobContext : IJobContext
    {
        private Dictionary<string, object> _internalDictionary = new Dictionary<string, object>();

        public object this[string index]
        {
            get
            {
                return _internalDictionary[index];
            }

            set
            {
                _internalDictionary[index]=value;
            }
        }

        public bool ContinueChain { get; set; } = true;

        public dynamic Settings { get; set; } = new System.Dynamic.ExpandoObject();
    }
}
