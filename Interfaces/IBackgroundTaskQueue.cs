using System;
using System.Threading;
using System.Threading.Tasks;

namespace OG_Visor_Service.Interfaces
{
    public interface IBackgroundTaskQueue
    {
        ValueTask QueueBackgroundWorkItemAsync(
    Func<CancellationToken, ValueTask> workItem);

        ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(
            CancellationToken cancellationToken);
    }
}