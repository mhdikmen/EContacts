namespace Report.API.Reports.GetReport
{
    public record GetReportResult(bool IsExists, Models.Report? Report = null);
}
