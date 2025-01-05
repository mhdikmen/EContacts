using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using Report.API.EventHandlers;
using Report.API.Events;
using Report.API.Services.Report;

namespace Report.API.Tests.UnitTests.EventHandlerTests
{
    public class ReportRequestedEventFaultHandlerTests
    {
        [Fact]
        public async Task Consume_Should_Call_SetReportStateAsFailedByIdAsync()
        {
            var mockLogger = new Mock<ILogger<ReportRequestedEventFaultHandler>>();
            var mockReportService = new Mock<IReportService>();
            var handler = new ReportRequestedEventFaultHandler(mockLogger.Object, mockReportService.Object);

            var reportId = Guid.NewGuid();
            var mockConsumeContext = new Mock<ConsumeContext<Fault<ReportCreatedEvent>>>();

            var reportCreatedEvent = new ReportCreatedEvent(reportId);
            mockConsumeContext
                .Setup(c => c.Message.Message)
                .Returns(reportCreatedEvent);

            await handler.Consume(mockConsumeContext.Object);

            mockReportService.Verify(
                s => s.SetReportStateAsFailedByIdAsync(reportId),
                Times.Once
            );
        }
    }
}
