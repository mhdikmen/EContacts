using Clean.Architecture.FunctionalTests;
using Contact.API.Contacts.CreateContact;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Contact.API.FunctionalTests.ApiEndpoints
{
    [Collection("Sequential")]
    public class CreateContactEndpoint(CustomWebApplicationFactory<Contact.API.Program> factory) : IClassFixture<CustomWebApplicationFactory<Contact.API.Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task Should_Return_201()
        {
            var request = new CreateContactRequest
            {
                Name = "John",
                Surname = "Doe",
                CompanyName = "ABC Systems"
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(CreateContactRequest.Route, content);
            var responseMessage = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreateContactResponse>(responseMessage);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }


        [Fact]
        public async Task Should_Return_400()
        {
            var request = new CreateContactRequest
            {
                Name = "John",
                Surname = "Doe"
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(CreateContactRequest.Route, content);
            var responseMessage = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreateContactResponse>(responseMessage);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
