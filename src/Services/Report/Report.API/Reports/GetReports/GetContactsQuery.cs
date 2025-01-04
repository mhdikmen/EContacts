using BuildingBlocks.Pagination;

namespace Report.API.Reports.GetReports;

public record GetReportsQuery : PaginationRequest, IQuery<GetReportsQueryResult>;