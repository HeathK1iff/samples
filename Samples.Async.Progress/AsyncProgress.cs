namespace Samples.Async.Progress
{
    public class AsyncProgress
    {
        public async Task LongWork(IProgress<int> progress, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                for (int i = 0; i <= 10; i++)
                {
                    Task.Delay(1000);
                    progress.Report(i * 10);
                }
            }, cancellationToken);
        }
    }
}