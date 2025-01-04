using BuildingBlocks.Pagination;

namespace Report.API.Services.Report
{
    public interface IReportService
    {
        Task<Models.Report?> GetReportByIdAsync(Guid id);
        Task<Models.Report> CreateReportAsync(Guid id);
        Task PrepareReportByIdAsync(Guid id);
        Task SetReportStateAsFailedByIdAsync(Guid id);
        Task<PaginatedResult<Models.Report>> GetReportsPaginatedAsync(int pageIndex, int pageSize);
    }
}
