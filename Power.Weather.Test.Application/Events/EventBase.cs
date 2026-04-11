using MediatR;

namespace Power.Weather.Test.Application.Events;

public abstract class EventBase : IRequest
{
}

public abstract class EventBase<TResult> : IRequest<TResult>
    where TResult : class
{
}