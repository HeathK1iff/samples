using System.Reflection;

ISemaphore semVar = new SemaphoreVariable();
semVar.Run();
Console.WriteLine(semVar.GetValue());

public class SemaphoreVariable : ISemaphore
{
    private Semaphore _semaphore = new Semaphore(initialCount: 1, maximumCount: 3);
    private int _value;

    public int GetValue()
    {
        return _value;
    }

    private void IncrementWithDelay(int sec)
    {
        var thread = new Thread(()=>{
            Console.WriteLine($"Start task {Thread.CurrentThread.Name}");
            _semaphore.WaitOne();
            try
            {
                Console.WriteLine($"Delay task {Thread.CurrentThread.Name}");
                Thread.Sleep(TimeSpan.FromSeconds(sec));
                Console.WriteLine($"Set value {Thread.CurrentThread.Name}, value {_value}");
                 _value = _value + 1;
            } 
            finally
            {
                _semaphore.Release();
            }
            Console.WriteLine($"End task {Thread.CurrentThread.Name}");
        });
        
        thread.Start();
        
    }

     public void Run()
    {
        IncrementWithDelay(1);
        IncrementWithDelay(0);
        IncrementWithDelay(1);
        Thread.Sleep(4000);
        
    }
}

public interface ISemaphore
{
   int GetValue(); 
   void Run();
}
