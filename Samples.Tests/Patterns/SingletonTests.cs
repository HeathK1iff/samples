using Samples.Singleton;

namespace Samples.Tests.Patterns
{
    [TestFixture()]
    public class SingletonTests
    {
        [Test()]
        public void PlusTest()
        {
            double expected = 15.0;
            double actual = Calculator.Instance.Plus(10, 5);

            Assert.AreEqual(expected, actual);
        }
    }
}