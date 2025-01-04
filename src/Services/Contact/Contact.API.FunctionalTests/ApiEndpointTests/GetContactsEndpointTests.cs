using Contact.API.Contacts.CreateContact;
using Contact.API.Contacts.DeleteContact;
using Contact.API.Contacts.GetContact;
using Contact.API.Contacts.GetContacts;

namespace Contact.API.FunctionalTests.ApiEndpoints
{
    [Collection("Sequential")]
    public class GetContactsEndpointTests(CustomWebApplicationFactory<Contact.API.Program> factory) : IClassFixture<CustomWebApplicationFactory<Contact.API.Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task Should_Return_400()
        {
            var getContactsResponseMessage = await _client.GetAsync(GetContactsRequest.BuidRoute(0, 0));
            getContactsResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Should_Return_200()
        {
            var createContactRequest = new CreateContactRequest
            {
                Name = "John",
                Surname = "Doe",
                CompanyName = "ABC Systems"
            };
            var createContactRequestContent = new StringContent(JsonConvert.SerializeObject(createContactRequest), Encoding.UTF8, "application/json");
            var createContactResponseMessage = await _client.PostAsync(CreateContactRequest.BuildRoute(), createContactRequestContent);
            createContactResponseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            var location = createContactResponseMessage.Headers.Location;

            location.Should().NotBeNull();

            var getContactsResponseMessage = await _client.GetAsync(GetContactsRequest.BuidRoute(0,1));
            getContactsResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

            var getContactsResponseContent = await getContactsResponseMessage.Content.ReadAsStringAsync();
            var getContactsResponse = JsonConvert.DeserializeObject<GetContactsResponse>(getContactsResponseContent);

            getContactsResponse.Should().NotBeNull();

            getContactsResponse!.Contacts.PageIndex.Should().Be(0);
            getContactsResponse.Contacts.PageSize.Should().Be(1);
        }
    }
}
