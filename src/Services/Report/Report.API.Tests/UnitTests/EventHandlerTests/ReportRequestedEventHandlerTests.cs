using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using Report.API.EventHandlers;
using Report.API.Events;
using Report.API.Services.Report;

namespace Report.API.Tests.UnitTests.EventHandlerTests
{
    public class ReportRequestedEventHandlerTests
    {
        [Fact]
        public async Task Consume_Should_Call_PrepareReportByIdAsync()
        {
            var mockLogger = new Mock<ILogger<ReportRequestedEventHandler>>();
            var mockReportService = new Mock<IReportService>();
            var handler = new ReportRequestedEventHandler(mockLogger.Object, mockReportService.Object);

            var reportId = Guid.NewGuid();
            var reportCreatedEvent = new ReportCreatedEvent(reportId);

            var mockConsumeContext = new Mock<ConsumeContext<ReportCreatedEvent>>();

            mockConsumeContext
                .Setup(c => c.Message)
                .Returns(reportCreatedEvent);

            await handler.Consume(mockConsumeContext.Object);

            mockReportService.Verify(
                s => s.PrepareReportByIdAsync(reportId),
                Times.Once
            );
        }
    }
}
