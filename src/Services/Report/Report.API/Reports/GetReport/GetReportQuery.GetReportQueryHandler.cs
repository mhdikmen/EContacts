using MassTransit;
using Report.API.Data;
using Report.API.Dtos.ReportDtos;
using Report.API.Events;

namespace Report.API.Reports.GetReport
{
    public class GetReportQueryHandler(IReportRepository _reportRepository)
        : BuildingBlocks.CQRS.IQueryHandler<GetReportQuery, GetReportResult>
    {
        public async Task<GetReportResult> Handle(GetReportQuery request, CancellationToken cancellationToken)
        {
            Models.Report? report = await _reportRepository.GetReportByIdAsync(request.Id);
            if (report is null)
                return new GetReportResult(false);

            return new GetReportResult(true, report);
        }
    }
}