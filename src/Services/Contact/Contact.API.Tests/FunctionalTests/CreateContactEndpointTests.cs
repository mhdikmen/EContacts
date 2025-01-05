using Contact.API.Contacts.CreateContact;
using Contact.API.Tests;

namespace Contact.API.Tests.FunctionalTests
{
    [Collection("Sequential")]
    public class CreateContactEndpoint(CustomWebApplicationFactory<Program> factory) : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Theory]
        [InlineData("John", "Doe", "ABC Systems", HttpStatusCode.Created)]
        [InlineData("John", "Doe", null, HttpStatusCode.BadRequest)]
        public async Task Should_Handle_CreateContact_Request(string name, string surname, string companyName, HttpStatusCode expectedStatusCode)
        {
            var request = new CreateContactRequest
            {
                Name = name,
                Surname = surname,
                CompanyName = companyName
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(CreateContactRequest.Route, content);
            response.StatusCode.Should().Be(expectedStatusCode);
        }
    }
}