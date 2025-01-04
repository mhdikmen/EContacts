using BuildingBlocks.Pagination;
using Report.API.Dtos.ReportDtos;

namespace Report.API.Reports.GetReports
{
    public record GetReportsResponse(PaginatedResult<ReportDto> Reports);
}
