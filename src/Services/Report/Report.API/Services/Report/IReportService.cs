namespace Report.API.Services.Report
{
    public interface IReportService
    {
        Task CreateReportByIdAsync(Guid Id);
        Task SetReportStateAsFailedByIdAsync(Guid Id);
    }
}
