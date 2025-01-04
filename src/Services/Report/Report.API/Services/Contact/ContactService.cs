using BuildingBlocks.Dtos.ContactDtos;
using Newtonsoft.Json;
using Report.API.Dtos.ContactDtos;

namespace Report.API.Services.Contact
{
    public class ContactService : IContactService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ContactService(IHttpClientFactory clientFactory)
        {
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
                HttpClient client = _httpClientFactory.CreateClient("Contact");
                HttpResponseMessage response = await client.GetAsync($"/contacts?pageIndex={currentPageIndex}&pageSize={pageSize}");
                response.EnsureSuccessStatusCode();
                string responseMessage = await response.Content.ReadAsStringAsync();
                GetReportsDto resp = JsonConvert.DeserializeObject<GetReportsDto>(responseMessage) ?? throw new Exception("Response is null");
                pageCount = (resp?.Contacts?.PageCount).GetValueOrDefault();

                if ((resp?.Contacts?.Data?.Count()).GetValueOrDefault() != 0)
                    contactDtos.AddRange(resp!.Contacts.Data);

                currentPageIndex++;
            } while (currentPageIndex <= pageCount);

            return contactDtos;
        }
    }
}
