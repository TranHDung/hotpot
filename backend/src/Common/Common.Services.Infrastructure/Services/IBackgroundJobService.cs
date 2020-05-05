using System.Threading.Tasks;

namespace Common.Services.Infrastructure.Services
{
    public interface IBackgroundJobService
    {
        void Enqueue();
        void Schedule();
        void Recurring();
        void Continuations();
    }
}
