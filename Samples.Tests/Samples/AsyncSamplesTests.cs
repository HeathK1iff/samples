using Newtonsoft.Json.Linq;
using Samples.Async;
using System.Diagnostics;

namespace Samples.Tests.Async
{
    [TestFixture]
    public class AsyncSamplesTests
    {
        [Test]
        public void ReadAsync_ReadWith6SecReadTimeOut_True()
        {
            var reader = new AsyncTimeOutReader();
            Assert.Throws<TimeoutException>(() => {

                try
                {
                    int result = reader.SomeWork10SecAsync((int) TimeSpan.FromSeconds(6).TotalMilliseconds).Result;
                } catch (AggregateException ex)
                {
                    throw ex.InnerException ?? new Exception();
                }
                
            });
        }
        [Test]
        public void ReadAsync_ReadWith11SecReadTimeOut_True()
        {
            var reader = new AsyncTimeOutReader();

            Stopwatch sw = new Stopwatch();
            sw.Start(); 
            int value = reader.SomeWork10SecAsync((int) TimeSpan.FromSeconds(12).TotalMilliseconds).Result;
            sw.Stop();

            Assert.IsTrue(value == 999);
            Assert.IsTrue(sw.ElapsedMilliseconds > TimeSpan.FromSeconds(10).Milliseconds);
        }

    }
}
