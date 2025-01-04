namespace Report.API.Reports.GetReport
{
    public record GetReportRequest
    {
        public const string Route = "/reports";
        public Guid Id { get; set; }
    }
}
