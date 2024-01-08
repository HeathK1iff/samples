using System.Text;

namespace Samples.TemplateMethod
{
    public class SimpleReport : DeviceReport
    {
        protected override string MakeBody(Device[] devices)
        {
            var sb = new StringBuilder();

            foreach (var device in devices)
            {
                sb.AppendLine($"{device.Id.ToString()}|{device.Name}|{device.Description}|{device.LastAccess.ToString("yyyy-MM-dd")}");
            }

            return sb.ToString();   
        }

        protected override string MakeCaption()
        {
            return "Device Report";
        }

        protected override string MakeFooter(Device[] devices)
        {
            return "Created By ReportMaster";
        }

        protected override string MakeHeader(Device[] devices)
        {
            return "Id|Name|Description|LastAccess";
        }
    }
}