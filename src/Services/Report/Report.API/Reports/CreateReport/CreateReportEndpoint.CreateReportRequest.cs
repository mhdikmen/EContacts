namespace Report.API.Reports.CreateReport
{
    public record CreateReportRequest
    {
        public const string Route = "/reports";
        public static string BuildRoute() => Route;
        public Guid Id { get; set; }
    }
}
