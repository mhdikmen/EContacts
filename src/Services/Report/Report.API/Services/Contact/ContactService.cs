using BuildingBlocks.Dtos.ContactDtos;
using Newtonsoft.Json;
using Report.API.Dtos.ContactDtos;

namespace Report.API.Services.Contact
{
    public class ContactService : IContactService
    {
        private readonly ILogger<ContactService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public ContactService(ILogger<ContactService> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _httpClientFactory = clientFactory;
        }
        public async Task<IList<ContactDto>> GetReportsAsync()
        {
            List<ContactDto> contactDtos = [];
            int pageSize = 1000;
            long pageCount = 0;
            long currentPageIndex = 0;
            do
            {
                _logger.LogInformation($"Fetching started for contacts from page {currentPageIndex}");
                HttpClient client = _httpClientFactory.CreateClient("Contact");
                HttpResponseMessage response = await client.GetAsync($"contacts?pageIndex={currentPageIndex}&pageSize={pageSize}");
                response.EnsureSuccessStatusCode();
                string responseMessage = await response.Content.ReadAsStringAsync();
                GetReportsDto resp = JsonConvert.DeserializeObject<GetReportsDto>(responseMessage) ?? throw new Exception("Response is null");
                pageCount = (resp?.Contacts?.PageCount).GetValueOrDefault();

                if ((resp?.Contacts?.Data?.Count()).GetValueOrDefault() != 0)
                    contactDtos.AddRange(resp!.Contacts.Data);
                _logger.LogInformation($"Fetching completed for contacts from page {currentPageIndex}");
                currentPageIndex++;
            } while (currentPageIndex < pageCount);

            return contactDtos;
        }
    }
}
