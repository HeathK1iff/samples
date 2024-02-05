using Moq;
using System.Text;
using Samples.Builder;

namespace Samples.Tests.Patterns
{
    [TestFixture]
    internal class BuilderTests
    {
        [Test]
        public void BuilderTest()
        {
            var headerDataSource = new Mock<IDataSource>();
            headerDataSource.Setup(f => f.GetData()).Returns(new string[] { "first name", "last name" });
            var bodyDataSource = new Mock<IDataSource>();
            bodyDataSource.Setup(f => f.GetData()).Returns(new string[] { "Joker", "Seed" });
            var reportGenerator = new ReportGenerator(new ReportBuilder(headerDataSource.Object, bodyDataSource.Object));
            var sb = new StringBuilder();
            sb.AppendLine("'first name', 'last name'");
            sb.AppendLine("'Joker', 'Seed'");
            sb.AppendLine("1");
            string expected = sb.ToString();

            string actual = reportGenerator.Build();

            Assert.IsTrue(expected.Equals(actual));            
        }
    }
}
