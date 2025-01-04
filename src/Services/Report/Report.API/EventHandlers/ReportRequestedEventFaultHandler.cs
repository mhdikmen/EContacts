using MassTransit;
using Report.API.Events;
using Report.API.Services.Report;

namespace Report.API.EventHandlers
{
    public class ReportRequestedEventFaultHandler(ILogger<ReportRequestedEventFaultHandler> _logger, IReportService _reportService) : IConsumer<Fault<ReportCreatedEvent>>
    {
        public async Task Consume(ConsumeContext<Fault<ReportCreatedEvent>> context)
        {
			try
			{
                await _reportService.SetReportStateAsFailedByIdAsync(context.Message.Message.ReportId);
            }
			catch (Exception ex)
			{
                _logger.LogError(ex, ex.Message);
                throw;
			}
        }
    }

}
