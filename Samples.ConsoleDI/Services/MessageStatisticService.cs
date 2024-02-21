using System.Collections.Concurrent;

public class MessageStatisticService : IMessageStatisticService, IDisposable
{
    private ConcurrentQueue<string> _queue = new();
    private int _analizeCapacity;

    public MessageStatisticService(int analizeCapacity)
    {
        _analizeCapacity = analizeCapacity;
        Console.WriteLine("Created Message Statistic Service");
    }

    public void AddMessage(string message)
    {
        _queue.Enqueue(message);

        if (_queue.Count > _analizeCapacity)
        {
            var analizeList = new List<string>();
            while (analizeList.Count < _analizeCapacity)
            {
                if (_queue.TryDequeue(out var msg))
                {
                    analizeList.Add(msg);
                }
            }
            Analize(analizeList);
        }
    }

    private void Analize(List<string> messages)
    {
        ;
    }

    public void Dispose()
    {
        Console.WriteLine("Disposed MessageStatisticService");
    }

    public int FailedMessages { get; private set; }
    public int SuccessMessages { get; private set; }
}
