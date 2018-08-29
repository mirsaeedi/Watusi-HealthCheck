using System.Threading.Tasks;

namespace Watusi.Middlewares
{
    public interface IMiddleware
    {
        Task Run(IJobContext jobContext);
    }
}
