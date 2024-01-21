using Moq;
using Samples.Command;

namespace Samples.Tests.Patterns
{
    [TestFixture()]
    public class CommandTests
    {
        [Test()]
        public void CommandTest()
        {
            var lamp = new Mock<ILamp>();
            lamp.Setup(f => f.SetOn()).Verifiable();

            var invoker = new CommandInvoker();
            invoker.SetCommand(new LedOnCommand(lamp.Object));
            invoker.InvokeAll();
            
            lamp.Verify();
        }
    }
}