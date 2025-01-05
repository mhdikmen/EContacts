using Report.API.Data;
using Report.API.Events;
using Report.API.Services.Contact;
using Report.API.Services.Report;

namespace Report.API.Tests.Services
{
    public class ReportServiceTests
    {
        private readonly Mock<IContactService> _mockContactService;
        private readonly Mock<IReportRepository> _mockReportRepository;
        private readonly Mock<IPublishEndpoint> _mockPublishEndpoint;
        private readonly ReportService _reportService;

        public ReportServiceTests()
        {
            _mockContactService = new Mock<IContactService>();
            _mockReportRepository = new Mock<IReportRepository>();
            _mockPublishEndpoint = new Mock<IPublishEndpoint>();
            _reportService = new ReportService(
                _mockContactService.Object,
                _mockReportRepository.Object,
                _mockPublishEndpoint.Object
            );
        }

        [Fact]
        public async Task GetReportByIdAsync_ShouldReturnReport_WhenReportExists()
        {
            var reportId = Guid.NewGuid();
            var expectedReport = new Models.Report(reportId);
            _mockReportRepository.Setup(r => r.GetReportByIdAsync(reportId)).ReturnsAsync(expectedReport);

            var result = await _reportService.GetReportByIdAsync(reportId);

            result.Should().BeEquivalentTo(expectedReport);
        }

        [Fact]
        public async Task CreateReportAsync_ShouldCreateReportAndPublishEvent()
        {
            var reportId = Guid.NewGuid();
            _mockReportRepository.Setup(r => r.CreateReportAsync(It.IsAny<Models.Report>())).Returns(Task.CompletedTask);
            var result = await _reportService.CreateReportAsync(reportId);
            result.Id.Should().Be(reportId);
        }

        [Fact]
        public async Task GetReportsPaginatedAsync_ShouldReturnPaginatedReports()
        {
            var pageIndex = 1;
            var pageSize = 10;
            var paginatedResult = new PaginatedResult<Models.Report>(pageIndex, pageSize, 1, 1, new List<Models.Report> { new Models.Report(Guid.NewGuid()) });
            _mockReportRepository.Setup(r => r.GetReportsPaginatedAsync(pageIndex, pageSize)).ReturnsAsync(paginatedResult);


            var result = await _reportService.GetReportsPaginatedAsync(pageIndex, pageSize);
            result.Should().BeEquivalentTo(paginatedResult);
        }

        [Fact]
        public async Task PrepareReportByIdAsync_ShouldPrepareReport_WhenReportExists()
        {
            var reportId = Guid.NewGuid();
            var report = new Models.Report(reportId);
            var contacts = new List<ContactDto>
            {
                new ContactDto
                {
                    ContactDetails = new List<ContactDetailDto>
                    {
                        new ContactDetailDto { Type = BuildingBlocks.Enums.ContactDetailType.Location, Content = "Location1" },
                        new ContactDetailDto { Type = BuildingBlocks.Enums.ContactDetailType.PhoneNumber, Content = "123" }
                    }
                },
                new ContactDto
                {
                    ContactDetails = new List<ContactDetailDto>
                    {
                        new ContactDetailDto { Type = BuildingBlocks.Enums.ContactDetailType.Location, Content = "Location1" },
                        new ContactDetailDto { Type = BuildingBlocks.Enums.ContactDetailType.PhoneNumber, Content = "456" }
                    }
                }
            };

            _mockReportRepository.Setup(r => r.GetReportByIdAsync(reportId)).ReturnsAsync(report);
            _mockContactService.Setup(c => c.GetReportsAsync()).ReturnsAsync(contacts);
            _mockReportRepository.Setup(r => r.UpdateReportAsync(reportId, It.IsAny<Models.Report>())).Returns(Task.CompletedTask);

            await _reportService.PrepareReportByIdAsync(reportId);

            report.State.Should().Be(BuildingBlocks.Enums.ReportState.Completed);
            report.CompletedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _mockReportRepository.Verify(r => r.UpdateReportAsync(reportId, It.Is<Models.Report>(r => r.State == BuildingBlocks.Enums.ReportState.Completed)), Times.Once);
        }

        [Fact]
        public async Task SetReportStateAsFailedByIdAsync_ShouldSetReportStateToFailed()
        {
            var reportId = Guid.NewGuid();
            var report = new Models.Report(reportId);
            _mockReportRepository.Setup(r => r.GetReportByIdAsync(reportId)).ReturnsAsync(report);
            _mockReportRepository.Setup(r => r.UpdateReportAsync(reportId, It.IsAny<Models.Report>())).Returns(Task.CompletedTask);
            
            await _reportService.SetReportStateAsFailedByIdAsync(reportId);

            report.State.Should().Be(BuildingBlocks.Enums.ReportState.Failed);
            _mockReportRepository.Verify(r => r.UpdateReportAsync(reportId, It.Is<Models.Report>(r => r.State == BuildingBlocks.Enums.ReportState.Failed)), Times.Once);
        }
    }
}
