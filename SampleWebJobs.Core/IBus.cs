using System.Threading.Tasks;

namespace SampleWebJobs.Core
{
    public interface IBus
    {
        Task PublishAsync(IMessage message);
    }
}
