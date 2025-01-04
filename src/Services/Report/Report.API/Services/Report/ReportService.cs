using BuildingBlocks.Dtos.ContactDtos;
using BuildingBlocks.Pagination;
using MassTransit;
using Report.API.Data;
using Report.API.Events;
using Report.API.Services.Contact;

namespace Report.API.Services.Report
{
    public class ReportService : IReportService
    {
        private readonly IContactService _contactService;
        private readonly IReportRepository _reportRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public ReportService(IContactService contactService, IReportRepository reportRepository, IPublishEndpoint publishEndpoint)
        {
            _contactService = contactService;
            _reportRepository = reportRepository;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<Models.Report?> GetReportByIdAsync(Guid id)
        {
            return await _reportRepository.GetReportByIdAsync(id);
        }

        public async Task<Models.Report> CreateReportAsync(Guid id)
        {
            var report = new Models.Report(id);
            await _reportRepository.CreateReportAsync(report);
            await _publishEndpoint.Publish(new ReportCreatedEvent(report.Id));
            return report;
        }

        public async Task<PaginatedResult<Models.Report>> GetReportsPaginatedAsync(int pageIndex, int pageSize)
        {
            PaginatedResult<Models.Report> paginatedResult = await _reportRepository.GetReportsPaginatedAsync(pageIndex, pageSize);
            return paginatedResult;
        }

        public async Task PrepareReportByIdAsync(Guid id)
        {
            Models.Report report = await _reportRepository.GetReportByIdAsync(id) ?? throw new InvalidOperationException("Report not found. Cannot prepare report.");
            List<ContactDto> contacts = [.. (await _contactService.GetReportsAsync())];
            List<string> locations = contacts
                .SelectMany(x => x.ContactDetails)
                .Where(x => x.Type == BuildingBlocks.Enums.ContactDetailType.Location)
                .Select(x => x.Content)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            locations.ForEach(location =>
            {
                IEnumerable<ContactDto> contactsInLocation = contacts.Where(x => x.ContactDetails.Any(y => y.Type == BuildingBlocks.Enums.ContactDetailType.Location && y.Content == location));
                long contactCount = contactsInLocation.Count();
                long phoneNumberCount = contactsInLocation.SelectMany(x => x.ContactDetails).Count(x => x.Type == BuildingBlocks.Enums.ContactDetailType.PhoneNumber);
                report.AddReportDetail(new(location, contactCount, phoneNumberCount));
            });

            report.State = BuildingBlocks.Enums.ReportState.Completed;
            report.CompletedDate = DateTime.UtcNow;
            await _reportRepository.UpdateReportAsync(id, report);
        }

        public async Task SetReportStateAsFailedByIdAsync(Guid id)
        {
            Models.Report report = await _reportRepository.GetReportByIdAsync(id) ?? throw new InvalidOperationException("Report not found. Cannot set the state of report.");
            report.State = BuildingBlocks.Enums.ReportState.Failed;
            await _reportRepository.UpdateReportAsync(id, report);
        }
    }
}
