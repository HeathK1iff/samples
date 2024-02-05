namespace Samples.Builder
{
    public class ReportBuilder : IReportBuilder
    {
        private readonly IDataSource _headerSource;
        private readonly IDataSource _bodySource;
        
        public ReportBuilder(IDataSource headerSource, IDataSource bodySource) 
        {
            _headerSource = headerSource;
            _bodySource = bodySource;
        }

        public string BuildHeader()
        {
            return $"'{_headerSource.GetData()[0]}', '{_headerSource.GetData()[1]}'";
        }

        public string BuildBody()
        {
            return $"'{_bodySource.GetData()[0]}', '{_bodySource.GetData()[1]}'";
        }

        public string BuildFooter()
        {
            return "1";
        }

    }
}
