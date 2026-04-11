namespace Power.Weather.Test.Application.Events;

public abstract class ListResult<TTotal, TRes> : BaseQueryResult
    where TRes : class
{
    protected ListResult(TTotal total, params TRes[] data)
    {
        Data = data;
        Total = total;
    }

    public TTotal Total { get; set; }
    public TRes[] Data { get; set; }
}

public abstract class ListResult<TRes> : ListResult<long, TRes>
    where TRes : class
{
    protected ListResult(long total, params TRes[] data) : base(total, data)
    {
    }
}