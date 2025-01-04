using MassTransit;
using Report.API.Events;
using Report.API.Services.Report;

namespace Report.API.EventHandlers
{
    public class ReportRequestedEventHandler(ILogger<ReportRequestedEventHandler> _logger, IReportService _reportService) : IConsumer<ReportCreatedEvent>
    {
        public async Task Consume(ConsumeContext<ReportCreatedEvent> context)
        {
            try
            {
                await _reportService.CreateReportByIdAsync(context.Message.ReportId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
