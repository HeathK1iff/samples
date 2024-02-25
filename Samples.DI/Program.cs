using Microsoft.Extensions.DependencyInjection;

ServiceCollection collection = new ServiceCollection();
collection.AddScoped<IScopedService, ScopedService>();
collection.AddSingleton<ISingletonService, SingletonService>();
collection.AddTransient<ITransientService, TransientService>();

using (var provider = collection.BuildServiceProvider())
{
    var singletonService = provider.GetRequiredService<ISingletonService>();

    var transientService = provider.GetRequiredService<ITransientService>();
    var transientService2 = provider.GetRequiredService<ITransientService>();
    

    using (var scopeServices = provider.CreateScope()){
        var scopeProvider = scopeServices.ServiceProvider;
        
        var targetService = scopeProvider.GetRequiredService<IScopedService>();
        targetService.Click();
    }

    singletonService.Click();
}

public interface IScopedService
{
    void Click();
}

public class ScopedService:  IScopedService, IDisposable
{
    public ScopedService()
    {
        Console.WriteLine($"Start {nameof(ScopedService)}");       
    }

    public void Click(){
        Console.WriteLine("ScopedService.Click was called");
    }

    public void Dispose()
    {
        Console.WriteLine($"Stop {nameof(ScopedService)}");
    }
}

public interface ISingletonService
{
    void Click(); 
}

public class SingletonService: ISingletonService, IDisposable
{
    public SingletonService()
    {
        Console.WriteLine($"Start {nameof(SingletonService)}");
    }
    
    public void Click(){
        Console.WriteLine("ScopedService.Click was called");
    }

    public void Dispose()
    {
        Console.WriteLine($"Stop {nameof(SingletonService)}");
    }
}

public interface ITransientService
{
    void Click(); 
}

public class TransientService: ITransientService, IDisposable
{
    public TransientService()
    {
        Console.WriteLine($"Start {nameof(TransientService)}");
    }
    
    public void Click(){
        Console.WriteLine("ScopedService.Click was called");
    }

    public void Dispose()
    {
        Console.WriteLine($"Stop {nameof(TransientService)}");
    }
}