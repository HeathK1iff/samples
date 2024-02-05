namespace Samples.Builder
{
    public interface IReportBuilder
    {
        string BuildBody();
        string BuildFooter();
        string BuildHeader();
    }
}