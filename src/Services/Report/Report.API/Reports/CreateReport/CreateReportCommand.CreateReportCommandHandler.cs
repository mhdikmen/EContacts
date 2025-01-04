using MassTransit;
using Report.API.Data;
using Report.API.Events;

namespace Report.API.Reports.CreateReport
{
    public class CreateReportCommandHandler(IReportRepository _reportRepository, IPublishEndpoint _publishEndpoint)
        : BuildingBlocks.CQRS.ICommandHandler<CreateReportCommand, CreateReportResult>
    {
        public async Task<CreateReportResult> Handle(CreateReportCommand request, CancellationToken cancellationToken)
        {
            var report = new Models.Report(request.Id);
            await _reportRepository.CreateReportAsync(report);
            await _publishEndpoint.Publish(new ReportCreatedEvent(report.Id));
            CreateReportResult result = report.Adapt<CreateReportResult>();
            return result;
        }
    }
}