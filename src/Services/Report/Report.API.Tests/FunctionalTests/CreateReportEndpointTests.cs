using BuildingBlocks.Dtos.ContactDtos;
using BuildingBlocks.Enums;
using BuildingBlocks.Pagination;
using FastEndpoints;
using Mongo2Go;
using MongoDB.Driver;
using Moq;
using Report.API.Dtos.ContactDtos;
using Report.API.Reports.CreateReport;
using Report.API.Services.Contact;
using Report.API.Tests;

namespace Report.API.Tests.FunctionalTests
{
    [Collection("Sequential")]
    public class CreateContactDetailEndpointTests(CustomWebApplicationFactory<Program> factory) : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task Should_Return_201()
        {
            var createReportRequest = new CreateReportRequest
            {
                Id = Guid.NewGuid()
            };
            var createReportRequestContent = new StringContent(JsonConvert.SerializeObject(createReportRequest), Encoding.UTF8, "application/json");
            var createReportResponseMessage = await _client.PostAsync(CreateReportRequest.BuildRoute(), createReportRequestContent);
            createReportResponseMessage.StatusCode.Should().Be(HttpStatusCode.Created);

            var location = createReportResponseMessage.Headers.Location;
            location.Should().NotBeNull();

            var createReportResponseContent = await createReportResponseMessage.Content.ReadAsStringAsync();
            createReportResponseContent.Should().NotBeNull();

            var createdReport = JsonConvert.DeserializeObject<CreateReportResponse>(createReportResponseContent);
            createdReport.Should().NotBeNull();

        }

        [Fact]
        public async Task Should_Return_400_If_Report_Exists()
        {
            var createReportRequest = new CreateReportRequest
            {
                Id = Guid.NewGuid()
            };
            var createReportRequestContent = new StringContent(JsonConvert.SerializeObject(createReportRequest), Encoding.UTF8, "application/json");
            var createReportResponseMessage = await _client.PostAsync(CreateReportRequest.BuildRoute(), createReportRequestContent);
            createReportResponseMessage.StatusCode.Should().Be(HttpStatusCode.Created);

            var location = createReportResponseMessage.Headers.Location;
            location.Should().NotBeNull();

            var createReportResponseContent = await createReportResponseMessage.Content.ReadAsStringAsync();
            createReportResponseContent.Should().NotBeNull();

            var createdReport = JsonConvert.DeserializeObject<CreateReportResponse>(createReportResponseContent);
            createdReport.Should().NotBeNull();

            var createReportResponseMessage2 = await _client.PostAsync(CreateReportRequest.BuildRoute(), createReportRequestContent);
            createReportResponseMessage2.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Should_Return_400_Invalid_Input()
        {
            var createReportRequest = new CreateReportRequest
            {
                Id = Guid.Empty
            };
            var createReportRequestContent = new StringContent(JsonConvert.SerializeObject(createReportRequest), Encoding.UTF8, "application/json");
            var createReportResponseMessage = await _client.PostAsync(CreateReportRequest.BuildRoute(), createReportRequestContent);
            createReportResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
