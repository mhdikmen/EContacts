namespace Report.API.Reports.GetReports
{
    public record GetReportsRequest
    {
        public const string Route = "/reports";
        public static string BuidRoute(int pageIndex, int pageSize) => $"/reports?pageIndex={pageIndex}&pageSize={pageSize}";
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}
