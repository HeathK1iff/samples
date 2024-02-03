namespace Samples.MessageBroker
{
    public class MessageBroker : IMessageBroker
    {
        private Dictionary<Type, List<ISubscriber>> _subscribers = new();
        public void Publish<T>(string message) where T : Message
        {
            if (!_subscribers.TryGetValue(typeof(T), out var subscriberList))
                return;

            subscriberList.ForEach(f => f.Receive(message));
        }

        public IDisposable Subscribe<T>(ISubscriber subscriber) where T : Message
        {
            if (!_subscribers.TryGetValue(typeof(T), out var subscriberList))
            {
                subscriberList = new List<ISubscriber>();
                _subscribers.Add(typeof(T), subscriberList);
            }

            subscriberList.Add(subscriber);

            return new Unsubscriber(subscriberList, subscriber);
        }

        private class Unsubscriber : IDisposable
        {
            private List<ISubscriber> _subscribers;
            private ISubscriber _subscriber;

            public Unsubscriber(List<ISubscriber> subscribers, ISubscriber subscriber)
            {
                _subscribers = subscribers;
                _subscriber = subscriber;
            }

            public void Dispose()
            {
                _subscribers.Remove(_subscriber);
                _subscriber = null;
            }
        }
    }
}
