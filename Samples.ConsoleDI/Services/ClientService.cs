public class ClientService : IClientService, IDisposable
{
    public ClientService() 
    {
        Console.WriteLine("Created ClientService");
    }

    public void Dispose()
    {
        Console.WriteLine("Disposed ClientService"); ;
    }
}
