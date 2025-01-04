using BuildingBlocks.Dtos.ContactDtos;
using Report.API.Data;
using Report.API.Services.Contact;

namespace Report.API.Services.Report
{
    public class ReportService : IReportService
    {
        private readonly IContactService _contactService;
        private readonly IReportRepository _reportRepository;

        public ReportService(IContactService contactService, IReportRepository reportRepository)
        {
            _contactService = contactService;
            _reportRepository = reportRepository;
        }
        public async Task CreateReportByIdAsync(Guid Id)
        {
            Models.Report report = await _reportRepository.GetReportByIdAsync(Id) ?? throw new Exception("Report not found");
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
            await _reportRepository.UpdateReportAsync(Id, report);
        }


        public async Task SetReportStateAsFailedByIdAsync(Guid Id)
        {
            Models.Report report = await _reportRepository.GetReportByIdAsync(Id) ?? throw new Exception("Report not found");
            report.State = BuildingBlocks.Enums.ReportState.Failed;
            await _reportRepository.UpdateReportAsync(Id, report);
        }
    }
}
