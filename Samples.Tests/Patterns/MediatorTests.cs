using Moq;
using NUnit.Framework;
using Samples.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.Tests.Patterns
{
    [TestFixture()]
    public class MediatorTests
    {
        [Test()]
        public void AlertMediatorTest()
        {
            var alertManager = new Mock<IAlert>();
            alertManager.Setup(f => f.DoAlert()).Verifiable();
            var swither = new Switcher();
            var mediator = new AlertMediator(swither, alertManager.Object);

            swither.SetOn();

            alertManager.Verify();
        }
    }
}