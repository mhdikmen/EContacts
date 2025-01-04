namespace Report.API.Reports.GetReport
{
    public record GetReportRequest
    {
        public const string Route = "/reports/{Id}";
        public Guid Id { get; set; }
    }
}
