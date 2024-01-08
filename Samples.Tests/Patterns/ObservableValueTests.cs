using Moq;
using Samples.Observer;

namespace Samples.Tests.Patterns
{
    [TestFixture()]
    public class ObservableValueTests
    {
        [Test()]
        public void SubscribeTest()
        {
            var observer = new Mock<IObserver<ObservableValue>>();
            observer.Setup(f => f.OnNext(It.IsAny<ObservableValue>())).Verifiable();
            var obj = new ObservableValue();
            obj.Subscribe(observer.Object);

            obj.CurrentValue = 1;

            observer.Verify();
        }
    }
}