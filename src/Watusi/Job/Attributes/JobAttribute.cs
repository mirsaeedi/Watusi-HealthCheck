using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watusi
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class JobAttribute : Attribute
    {
        public string Name { get; set; }

        public string CronExpression { get; set; }
    }
}
