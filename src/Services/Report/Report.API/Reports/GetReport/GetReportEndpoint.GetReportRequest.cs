namespace Report.API.Reports.GetReport
{
    public record GetReportRequest
    {
        public const string Route = "/reports/{Id}";
        public static string BuildRoute(Guid id) => $"/reports/{id}";
        public Guid Id { get; set; }
    }
}
