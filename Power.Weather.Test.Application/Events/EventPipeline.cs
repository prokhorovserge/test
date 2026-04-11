using MediatR;

namespace Power.Weather.Test.Application.Events;

public class EventPipeline : IEventPipeline
{
    private readonly IMediator _mediator;

    public EventPipeline(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<TRes> BroadcastAsync<TRes>(EventBase<TRes> message, CancellationToken cancellationToken = default)
        where TRes : class
    {
        throw new NotImplementedException();
    }

    public async Task<TRes> SendAsync<TRes>(EventBase<TRes> message, CancellationToken cancellationToken = default)
        where TRes : class
    {
        return await _mediator.Send(message, cancellationToken);
    }
}