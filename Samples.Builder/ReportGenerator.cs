using System.Text;

namespace Samples.Builder
{
    public class ReportGenerator
    {
        private readonly IReportBuilder _builder;
        public ReportGenerator(IReportBuilder builder) 
        {
            _builder = builder;
        }    

        public string Build()
        {
            var sb = new StringBuilder();
            sb.AppendLine(_builder.BuildHeader());
            sb.AppendLine(_builder.BuildBody());
            sb.AppendLine(_builder.BuildFooter());
            return sb.ToString();
        }

    }
}
