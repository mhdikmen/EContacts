namespace Report.API.Reports.CreateReport
{
    public record CreateReportRequest
    {
        public const string Route = "/reports";
        public Guid Id { get; set; }
    }
}
