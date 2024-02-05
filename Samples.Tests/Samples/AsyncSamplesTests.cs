using Moq;
using Newtonsoft.Json.Linq;
using Samples.Async;
using System.Diagnostics;

namespace Samples.Tests.Async
{
    [TestFixture]
    public class AsyncSamplesTests
    {
        [Test]
        public void SomeWork10SecAsync_ReadWith6SecReadTimeOut_Throw()
        {
            var worker = new AsyncTimeOutWorker();
            Assert.Throws<TimeoutException>(() => {

                try
                {
                    int result = worker.SomeWork10SecAsync(TimeSpan.FromSeconds(6)).Result;
                } catch (AggregateException ex)
                {
                    throw ex.InnerException ?? new Exception();
                }
                
            });
        }
        [Test]
        public async Task SomeWork10SecAsync_ReadWith11SecReadTimeOut_True()
        {
            var reader = new AsyncTimeOutWorker();

            Stopwatch sw = new Stopwatch();
            sw.Start(); 
            int value = await reader.SomeWork10SecAsync(TimeSpan.FromSeconds(12));
            sw.Stop();

            Assert.IsTrue(value == 999);
            Assert.IsTrue(sw.ElapsedMilliseconds > TimeSpan.FromSeconds(10).Milliseconds);
        }

        [Test]
        public async Task GetWorkDataAsync_ReturnValueFromInterface_True()
        {
            var mock = new Mock<IAsyncTimeOutWorker>();
            mock.Setup(f=>f.SomeWork10SecAsync(It.IsAny<TimeSpan>())).Returns(Task<int>.FromResult(999));
            var service = new TimeOutService(mock.Object);

            int result = await service.GetWorkDataAsync();

            Assert.IsTrue(result == 999);
        }

        [Test]
        public async Task GetWorkDataAsync_ThrowTimeOutExceptionFromInterface_Throw()
        {
            var mock = new Mock<IAsyncTimeOutWorker>();
            mock.Setup(f => f.SomeWork10SecAsync(It.IsAny<TimeSpan>()))
                .Returns(Task<int>.FromException<int>(new TimeoutException()));
            var service = new TimeOutService(mock.Object);
           
            Assert.Throws<AggregateException>(() => service.GetWorkDataAsync().Wait());
        }

    }
}
