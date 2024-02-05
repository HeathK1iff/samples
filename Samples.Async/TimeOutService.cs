using System.Reflection.PortableExecutable;

namespace Samples.Async
{
    public class TimeOutService
    {
        private readonly IAsyncTimeOutWorker _worker;
        public TimeOutService(IAsyncTimeOutWorker worker)
        {
            _worker = worker;
        }

        public async Task<int> GetWorkDataAsync()
        {
            return await _worker.SomeWork10SecAsync(TimeSpan.FromSeconds(10));
        }
    }
}
