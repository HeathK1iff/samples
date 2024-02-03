using Moq;
using Samples.MessageBroker;

namespace Samples.Tests.Patterns
{
    [TestFixture]
    public class MessageBrokerTests
    {
        private class MyMessage: Message
        { 
        
        }

        [Test]
        public void Publish_CheckReceiveMessage_Verify()
        {
            IMessageBroker broker = new MessageBroker.MessageBroker();
            var subscriber = new Mock<ISubscriber>();
            subscriber.Setup(f => f.Receive(It.Is<string>(f => f.Equals("Hello World", StringComparison.InvariantCulture)))).Verifiable();
            broker.Subscribe<MyMessage>(subscriber.Object);

            broker.Publish<MyMessage>("Hello World");

            subscriber.Verify();    
        }

        [Test]
        public void Publish_CheckMultiplyReceive_Verify()
        {
            IMessageBroker broker = new MessageBroker.MessageBroker();
            var subscriber = new Mock<ISubscriber>();
            subscriber.Setup(f => f.Receive(It.Is<string>(f => f.Equals("Hello World", StringComparison.InvariantCulture)))).Verifiable();
            var subscriber2 = new Mock<ISubscriber>();
            subscriber2.Setup(f => f.Receive(It.Is<string>(f => f.Equals("Hello World", StringComparison.InvariantCulture)))).Verifiable();
            broker.Subscribe<MyMessage>(subscriber.Object);
            broker.Subscribe<MyMessage>(subscriber2.Object);

            broker.Publish<MyMessage>("Hello World");

            subscriber.Verify();
            subscriber2.Verify();

        }
    }
}
