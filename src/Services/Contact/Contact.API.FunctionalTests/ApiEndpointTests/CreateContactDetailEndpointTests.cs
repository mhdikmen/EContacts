using Contact.API.Contacts.CreateContact;
using Contact.API.Contacts.CreateContactDetail;

namespace Contact.API.FunctionalTests.ApiEndpoints
{
    [Collection("Sequential")]
    public class CreateContactDetailEndpointTests(CustomWebApplicationFactory<Contact.API.Program> factory) : IClassFixture<CustomWebApplicationFactory<Contact.API.Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task Should_Return_200()
        {
            var createContactRequest = new CreateContactRequest
            {
                Name = "John",
                Surname = "Doe",
                CompanyName = "ABC Systems"
            };
            var createContactContent = new StringContent(JsonConvert.SerializeObject(createContactRequest), Encoding.UTF8, "application/json");
            var createContactResponse = await _client.PostAsync(CreateContactRequest.BuildRoute(), createContactContent);

            createContactResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var createContactResponseBody = await createContactResponse.Content.ReadAsStringAsync();
            createContactResponseBody.Should().NotBeNull();

            var createdContact = JsonConvert.DeserializeObject<CreateContactResponse>(createContactResponseBody);
            createdContact.Should().NotBeNull();

            var contactId = createdContact!.Id;
            contactId.Should().NotBeEmpty();

            var createContactDetailRequest = new CreateContactDetailRequest
            {
                ContactId = contactId,
                Type = ContactDetailType.PhoneNumber,
                Content = "123-456-7890"
            };
            var createContactDetailRequestContent = new StringContent(JsonConvert.SerializeObject(createContactDetailRequest), Encoding.UTF8, "application/json");
            var createContactDetailResponseMessage = await _client.PatchAsync(CreateContactDetailRequest.BuildRoute(createContactDetailRequest.ContactId), createContactDetailRequestContent);

            createContactDetailResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Fact]
        public async Task Should_Return_404()
        {
            var createContactDetailRequest = new CreateContactDetailRequest
            {
                ContactId = Guid.NewGuid(),
                Type = ContactDetailType.PhoneNumber,
                Content = "123-456-7890"
            };
            var createContactDetailRequestContent = new StringContent(JsonConvert.SerializeObject(createContactDetailRequest), Encoding.UTF8, "application/json");
            var createContactDetailResponseMessage = await _client.PatchAsync(CreateContactDetailRequest.BuildRoute(createContactDetailRequest.ContactId), createContactDetailRequestContent);

            createContactDetailResponseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
