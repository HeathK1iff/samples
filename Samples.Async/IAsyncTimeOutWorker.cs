namespace Samples.Async
{
    public interface IAsyncTimeOutWorker
    {
        Task<int> SomeWork10SecAsync(TimeSpan millisecondsReadTimeOut);
    }
}