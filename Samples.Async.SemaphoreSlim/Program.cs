using System.Reflection;
using System.Runtime.CompilerServices;

ISemaphore semVar = new SemaphoreSlimVariable();
semVar.Run();
Console.WriteLine(semVar.GetValue());


public interface ISemaphore
{
   int GetValue(); 
   void Run();
}


public class SemaphoreSlimVariable : ISemaphore
{
    private readonly SemaphoreSlim _slim = new SemaphoreSlim(1);
    private int _value;
    private async Task IncrementWithDelayAsync(int sec)
    {
        Console.WriteLine($"Start task {Thread.CurrentThread.Name}");
        await _slim.WaitAsync();
        try
        {
            await Task.Delay(TimeSpan.FromSeconds(sec));
            Console.WriteLine($"Set value {Thread.CurrentThread.Name}, value {_value}");
            _value = _value + 1;
        } 
        finally
        {
            _slim.Release();
        }
         Console.WriteLine($"End task {Thread.CurrentThread.Name}");
    }

    public int GetValue()
    {
        return _value;
    }

    public void Run()
    {
        Task task1 = IncrementWithDelayAsync(1);
        Task task2 = IncrementWithDelayAsync(0);
        Task task3 = IncrementWithDelayAsync(1);
        
        Task.WaitAll(task1, task2, task3);
    }
}