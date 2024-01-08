using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Samples.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.Tests.Patterns
{
    [TestFixture()]
    public class CompositeTests
    {
        [Test()]
        public void CompositeTest()
        {
            const string expected = @"{test:0,{test:{test1:1,test2:2}}";
            var stream = new MemoryStream();
            var writer = new JsonDataWriter(stream);
            writer.Add(new JsonKeyValue("test", "0"));
            writer.Add(new JsonCompositeKeyValue("test")
            {
                new JsonKeyValue("test1", "1"),
                new JsonKeyValue("test2", "2")
            });

            writer.Flush();
            string actual = ReadString(stream); 
            
            Assert.That(actual.Equals(expected), Is.True);
        }

        private string ReadString(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }


    }
}