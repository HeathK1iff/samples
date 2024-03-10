using Microsoft.Extensions.DependencyInjection;

ServiceCollection collection = new ServiceCollection();
collection.AddTransient<IService, Service>();

using (var provider = collection.BuildServiceProvider()){
    Console.WriteLine("GetRequiredService<>() - begin");
    var service = provider.GetRequiredService<IService>();
    if (service is IAsyncInitialization initializator){
        await initializator.Initialization;
    }
    Console.WriteLine("GetRequiredService<>() - end");
}

Console.WriteLine("Press any key!");
Console.ReadLine();

public interface IAsyncInitialization
{
    Task Initialization { get; }
}
public interface IService
{

}

public class Service: IService, IAsyncInitialization
{
    public Service(){
        Console.WriteLine("Service() - begin");
        Initialization = InitializationAsync();
        Console.WriteLine("Service() - end");
    }

    public Task Initialization { get; }

    public async Task InitializationAsync()
    {
        Console.WriteLine("Delay() - begin");
        await Task.Delay(2000);
        Console.WriteLine("Delay() - end");
    }

}