using Contact.API.Contacts.CreateContact;
using Contact.API.Contacts.DeleteContact;
using Contact.API.Contacts.GetContact;

namespace Contact.API.FunctionalTests.ApiEndpoints
{
    [Collection("Sequential")]
    public class GetContactEndpointTests(CustomWebApplicationFactory<Contact.API.Program> factory) : IClassFixture<CustomWebApplicationFactory<Contact.API.Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task Should_Return_400()
        {
            var getContactResponseMessage = await _client.GetAsync(GetContactRequest.BuildRoute(Guid.Empty));
            getContactResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


        [Fact]
        public async Task Should_Return_404()
        {
            var getContactResponseMessage = await _client.GetAsync(GetContactRequest.BuildRoute(Guid.NewGuid()));
            getContactResponseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
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

            var getContactResponseMessage = await _client.GetAsync(location);
            getContactResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
