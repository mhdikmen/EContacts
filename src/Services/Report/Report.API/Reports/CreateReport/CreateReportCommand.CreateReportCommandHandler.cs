using Report.API.Services.Report;

namespace Report.API.Reports.CreateReport
{
    public class CreateReportCommandHandler(IReportService _reportService)
        : BuildingBlocks.CQRS.ICommandHandler<CreateReportCommand, CreateReportResult>
    {
        public async Task<CreateReportResult> Handle(CreateReportCommand request, CancellationToken cancellationToken)
        {
            Models.Report? result = await _reportService.GetReportByIdAsync(request.Id);
            if (result is not null)
                return new CreateReportResult(false);

            Models.Report createdReport = await _reportService.CreateReportAsync(request.Id);
            return new CreateReportResult(true, createdReport.Id);
        }
    }
}