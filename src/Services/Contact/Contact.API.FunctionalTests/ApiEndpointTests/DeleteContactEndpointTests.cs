using Contact.API.Contacts.CreateContact;
using Contact.API.Contacts.DeleteContact;

namespace Contact.API.FunctionalTests.ApiEndpoints
{
    [Collection("Sequential")]
    public class DeleteContactEndpointTests(CustomWebApplicationFactory<Contact.API.Program> factory) : IClassFixture<CustomWebApplicationFactory<Contact.API.Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();


        [Fact]
        public async Task Should_Return_400()
        {
            var deleteContactResponseMessage = await _client.DeleteAsync(DeleteContactRequest.BuildRoute(Guid.Empty));
            deleteContactResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Should_Return_404()
        {
            var deleteContactResponseMessage = await _client.DeleteAsync(DeleteContactRequest.BuildRoute(Guid.NewGuid()));
            deleteContactResponseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }


        [Fact]
        public async Task Should_Return_204()
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

            var deleteContactResponseMessage = await _client.DeleteAsync(location);
            deleteContactResponseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
