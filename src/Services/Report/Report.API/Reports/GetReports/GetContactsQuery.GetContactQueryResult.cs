using BuildingBlocks.Pagination;

namespace Report.API.Reports.GetReports
{
    public record GetReportsQueryResult(PaginatedResult<Models.Report> Reports);
}
