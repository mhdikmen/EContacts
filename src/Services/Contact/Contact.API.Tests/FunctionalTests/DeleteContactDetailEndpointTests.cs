using Contact.API.Contacts.CreateContact;
using Contact.API.Contacts.CreateContactDetail;
using Contact.API.Contacts.DeleteContactDetail;
using Contact.API.Tests;

namespace Contact.API.Tests.FunctionalTests
{
    [Collection("Sequential")]
    public class DeleteContactDetailEndpointTests(CustomWebApplicationFactory<Program> factory) : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();


        [Fact]
        public async Task Should_Return_400()
        {
            var deleteContactResponseMessage = await _client.DeleteAsync(DeleteContactDetailRequest.BuildRoute(Guid.Empty, Guid.Empty));
            deleteContactResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Should_Return_404()
        {
            var deleteContactResponseMessage = await _client.DeleteAsync(DeleteContactDetailRequest.BuildRoute(Guid.NewGuid(), Guid.NewGuid()));
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

            var createContactResponseContent = await createContactResponseMessage.Content.ReadAsStringAsync();

            var createdContact = JsonConvert.DeserializeObject<CreateContactResponse>(createContactResponseContent);
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

            var createContactDetailResponseContent = await createContactDetailResponseMessage.Content.ReadAsStringAsync();
            createContactDetailResponseContent.Should().NotBeNull();

            var createContactDetailResponse = JsonConvert.DeserializeObject<CreateContactDetailResponse>(createContactDetailResponseContent);
            createContactDetailResponse.Should().NotBeNull();

            var createContactDetailId = createContactDetailResponse!.Id;
            createContactDetailId.Should().NotBeEmpty();

            var deleteContactResponseMessage = await _client.DeleteAsync(DeleteContactDetailRequest.BuildRoute(contactId, createContactDetailId));
            deleteContactResponseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
