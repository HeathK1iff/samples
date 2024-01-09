using NUnit.Framework;
using Samples.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.Tests.Patterns
{
    [TestFixture()]
    public class AdapterTests
    {
        [Test()]
        public void AdapterTest()
        {
            const string expected = "Test";
            string actual = string.Empty;

            using (Stream stream = new MemoryStream())
            {
                IExternalDevice converter = new StreamExternalDeviceAdapter(stream);
                converter.Write(expected);

                actual = converter.Read();
            }

            Assert.That(actual.Equals(expected), Is.True);
        }


    }
}