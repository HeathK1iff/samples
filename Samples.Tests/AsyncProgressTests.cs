using Moq;
using Samples.Async.Progress;

namespace Samples.Tests.Async
{
    [TestFixture]
    public class AsyncProgressTests
    {
        [Test]
        public async Task LongWork_CheckProgress_True()
        {
            int actual = 0;
            const int Expected = 100;
            var target = new AsyncProgress();
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            var progress = new Mock<IProgress<int>>();
            progress.Setup(f => f.Report(It.IsAny<int>())).Callback<int>(f => {
                actual = f;
            });

            await target.LongWork(progress.Object, token);

            Assert.AreEqual(Expected, actual);
        }
    }
}