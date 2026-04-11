using MediatR;

namespace Power.Weather.Test.Application.Events;

public abstract class BaseEventHandler<TReq, TResp> : IRequestHandler<TReq, TResp>
    where TReq : EventBase<TResp>
    where TResp : class
{
    public abstract Task<TResp> Handle(TReq request, CancellationToken cancellationToken);
}
