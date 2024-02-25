using System.Text.Json;

public class NotificationSenderService : INotificationSenderService, IDisposable
{
    private IMessageStatisticService _messageStatisticService;
    public NotificationSenderService(IMessageStatisticService messageStatisticService) 
    {
        _messageStatisticService = messageStatisticService;
        Console.WriteLine("Create Notification Service");
    }

    public void Dispose()
    {
        Console.WriteLine("Disposed Notification Service");
    }

    public async Task<string> SendAsync(Uri host, Notification notification, CancellationToken completionToken)
    {
        string json = JsonSerializer.Serialize(notification);

        using HttpClient client = new HttpClient();
        client.Timeout = TimeSpan.FromSeconds(1);


        var response = await client.SendAsync(new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            Content = new StringContent(json)
        }, completionToken);


        var body = response.Content.ReadAsStringAsync().Result;

        _messageStatisticService.AddMessage(body);

        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync(completionToken);

        return content;
    }
}
