
namespace Power.Weather.Test.Application.Events;

public interface IEventPipeline
{
    Task<TRes> SendAsync<TRes>(EventBase<TRes> message, CancellationToken cancellationToken = default)
        where TRes : class;

    Task<TRes> BroadcastAsync<TRes>(EventBase<TRes> message, CancellationToken cancellationToken = default)
        where TRes : class;
}
