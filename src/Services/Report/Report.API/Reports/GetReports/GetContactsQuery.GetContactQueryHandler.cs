using BuildingBlocks.Pagination;
using Report.API.Services.Report;

namespace Report.API.Reports.GetReports;

internal class GetReportsQueryHandler(IReportService _reportService)
    : IQueryHandler<GetReportsQuery, GetReportsQueryResult>
{
    public async Task<GetReportsQueryResult> Handle(GetReportsQuery request, CancellationToken cancellationToken)
    {
        PaginatedResult<Models.Report> paginatedResult = await _reportService.GetReportsPaginatedAsync(request.PageIndex, request.PageSize);
        return new GetReportsQueryResult(paginatedResult);
    }
}