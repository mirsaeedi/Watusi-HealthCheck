using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Watusi.Middlewares
{
    interface IMiddleware
    {
        Task Execute(IJobContext jobContext);
    }
}
