namespace Samples.MessageBroker
{
    public interface IMessageBroker
    {
        void Publish<T>(string message) where T : Message;
        IDisposable Subscribe<T>(ISubscriber subscriber) where T : Message;
    }
}