using BuildingBlocks.Pagination;
using Report.API.Data;

namespace Report.API.Reports.GetReports;

internal class GetReportsQueryHandler(IReportRepository _reportRepository)
    : IQueryHandler<GetReportsQuery, GetReportsQueryResult>
{
    public async Task<GetReportsQueryResult> Handle(GetReportsQuery request, CancellationToken cancellationToken)
    {
        PaginatedResult<Models.Report> paginatedResult = await _reportRepository.GetReportsPaginatedAsync(request.PageIndex, request.PageSize);
        return new GetReportsQueryResult(paginatedResult);
    }
}