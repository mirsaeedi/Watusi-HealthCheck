using System;
using System.Collections.Generic;
using System.Text;

namespace Watusi.Infrastructure
{
    public class HealthCheckParams<THealthCheckParams,TResult> 
    {
        public Action<string,THealthCheckParams, TResult> Notify { get; set; }
        public string GreenMessage { get; set; }
        public string RedMessage { get; set; }
    }
}
