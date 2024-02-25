public interface IMessageStatisticService
{
    int FailedMessages { get; }
    int SuccessMessages { get; }
    void AddMessage(string message);
}
