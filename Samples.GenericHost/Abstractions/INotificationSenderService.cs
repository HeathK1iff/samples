public interface INotificationSenderService
{
    Task<string> SendAsync(Uri host, Notification notification, CancellationToken completionToken);
}
