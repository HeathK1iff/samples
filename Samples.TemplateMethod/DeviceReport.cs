using System.Text;

namespace Samples.TemplateMethod
{
    public abstract class DeviceReport
    {
        public string GenerateReport(Device[] devices)
        {
            var sb = new StringBuilder();
            sb.AppendLine("   " + MakeCaption() + "   ");
            sb.AppendLine(MakeHeader(devices));
            sb.Append(MakeBody(devices));
            sb.AppendLine(MakeFooter(devices));
            return sb.ToString();
        }

        protected abstract string MakeCaption();
        protected abstract string MakeHeader(Device[] devices);
        protected abstract string MakeBody(Device[] devices);
        protected abstract string MakeFooter(Device[] devices);
    }
}