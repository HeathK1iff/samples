
Console.WriteLine("Application - Start");
var instance = await AsyncFactoryMethod.CreateAsync();
Console.WriteLine("Application - End");

public class AsyncFactoryMethod{
    private AsyncFactoryMethod(){
        Console.WriteLine("test");
    }

    private async Task<AsyncFactoryMethod> InitializationAsync(){
        await Task.Delay(2000);
        return this;
    }

    public static Task<AsyncFactoryMethod> CreateAsync(){
        var result = new AsyncFactoryMethod();
        Console.WriteLine("CreateAsync");
        return result.InitializationAsync();
    } 
}