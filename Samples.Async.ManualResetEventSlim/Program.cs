ManualResetEvent reset = new ManualResetEvent(true);
int val = 0;

Task task1 = PrintAsync(10);
Task task2 = PrintAsync(30);
Task task3 = PrintAsync(15);

Task.WaitAll(task1, task2, task3);

async Task PrintAsync(int num)
{
    reset.WaitOne();
    try
    {
        val = val + num;  
        Console.WriteLine(val);
        await Task.Delay(1000);
    }
    finally
    {
        reset.Reset();
    }
}



