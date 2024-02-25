using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Worker : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public Worker(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        ManualResetEvent resetEvet = new ManualResetEvent(false);

        while (!stoppingToken.IsCancellationRequested)
        {
            using var serviceScope = _serviceScopeFactory.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            Console.WriteLine("Attempt to send notification");

            INotificationSenderService notifyService = serviceProvider.GetRequiredService<INotificationSenderService>();
            INotificationSenderService notifyService2 = serviceProvider.GetRequiredService<INotificationSenderService>();
            IClientService service1 = serviceProvider.GetRequiredService<IClientService>();
            IClientService service2 = serviceProvider.GetRequiredService<IClientService>();

            var message = new Notification()
            {
                Sender = "John Wick",
                Message = "Do you need help?"
            };

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            try
            {
                Task task = notifyService.SendAsync(new Uri("http://localhost"), message, cancellationToken);

                task.Wait();

                Console.WriteLine(message);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            resetEvet.WaitOne(TimeSpan.FromSeconds(2));
        }

        return Task.CompletedTask;
    }
}
