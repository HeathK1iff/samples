using System.Runtime.CompilerServices;

namespace Samples.Async
{
    public class AsyncTimeOutReader
    {
        private const int TimeOfWork = 10000;

        public async Task<int> SomeWork10SecAsync(int millisecondsReadTimeOut)
        {
            using CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(millisecondsReadTimeOut);
            Task<int> task = CreateTaskWithSomeWork(999);
            Task timeOutTask = Task.Delay(Timeout.InfiniteTimeSpan, cancellationTokenSource.Token);

            Task result = await Task.WhenAny(task, timeOutTask);

            if (result == timeOutTask)
                throw new TimeoutException();

            return await task;
        }

        private Task<int> CreateTaskWithSomeWork(int returnValue)
        {
            return Task<int>.Factory.StartNew(() =>
            {
                Thread.Sleep(TimeOfWork);
                return returnValue;
            });
        }

    }
}