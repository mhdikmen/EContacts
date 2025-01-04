using BuildingBlocks.Pagination;

namespace Report.API.Data
{
    public interface IReportRepository
    {
        Task<Models.Report?> GetReportByIdAsync(Guid id);
        Task CreateReportAsync(Models.Report report);
        Task UpdateReportAsync(Guid id, Models.Report report);
        Task<PaginatedResult<Models.Report>> GetReportsPaginatedAsync(int pageIndex, int pageSize);
    }
}
