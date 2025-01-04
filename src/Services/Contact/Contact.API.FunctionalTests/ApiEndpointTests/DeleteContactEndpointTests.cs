using Contact.API.Contacts.CreateContact;
using Contact.API.Contacts.CreateContactDetail;
using Contact.API.Contacts.DeleteContact;
using FastEndpoints;

namespace Contact.API.FunctionalTests.ApiEndpoints
{
    [Collection("Sequential")]
    public class DeleteContactEndpointTests(CustomWebApplicationFactory<Contact.API.Program> factory) : IClassFixture<CustomWebApplicationFactory<Contact.API.Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task Should_Return_404()
        {
            var deleteContactRequest = new DeleteContactRequest
            {
                Id = Guid.NewGuid()
            };

            var deleteContactRequestContent = new StringContent(JsonConvert.SerializeObject(deleteContactRequest), Encoding.UTF8, "application/json");
            var deleteContactResponseMessage = await _client.PostAsync(CreateContactRequest.BuildRoute(), deleteContactRequestContent);

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
