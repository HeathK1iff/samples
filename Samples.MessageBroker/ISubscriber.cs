namespace Samples.MessageBroker
{
    public interface ISubscriber
    {
        void Receive(string message);
    }
}
