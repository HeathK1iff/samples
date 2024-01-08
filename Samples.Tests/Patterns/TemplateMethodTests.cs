using Samples.TemplateMethod;

namespace Samples.Tests.Patterns
{
    [TestFixture()]
    public class TemplateMethodTests
    {
        [Test()]
        public void GenerateReport_MakeReport_True()
        {
            Guid firstId = Guid.NewGuid();
            Guid secondId = Guid.NewGuid();
            string expected = "   Device Report   \r\n" +
                              "Id|Name|Description|LastAccess\r\n" +
                              $"{firstId.ToString()}|TestName|TestDescription|2023-12-12\r\n" +
                              $"{secondId.ToString()}|TestName2|TestDescription2|2023-12-14\r\n" +
                              "Created By ReportMaster\r\n";
            var data = new Device[]
            {
                new Device()
                {
                    Id = firstId,
                    Description = "TestDescription",
                    LastAccess = new DateTime(2023, 12, 12),
                    Name = "TestName"
                },
                new Device()
                {
                    Id = secondId,
                    Description = "TestDescription2",
                    LastAccess = new DateTime(2023, 12, 14),
                    Name = "TestName2"
                }
            };
            DeviceReport report = new SimpleReport();

            var actual = report.GenerateReport(data);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}