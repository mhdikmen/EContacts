using Newtonsoft.Json;
using Report.API.Dtos.ReportDtos;
using Report.API.Reports.CreateReport;
using Report.API.Reports.GetReport;
using System.Text;

namespace Report.API.FunctionalTests.ApiEndpointTests
{
    [Collection("Sequential")]
    public class GetReportEndpointEndpointTests(CustomWebApplicationFactory<Report.API.Program> factory) : IClassFixture<CustomWebApplicationFactory<Report.API.Program>>
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

            var getReportResponseMessage = await _client.GetAsync(location);
            getReportResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            var getReportResponseContent = await getReportResponseMessage.Content.ReadAsStringAsync();
            var reportResponse = JsonConvert.DeserializeObject<GetReportResponse>(getReportResponseContent);
            reportResponse.Should().NotBeNull();
            reportResponse!.Report.Id.Should().Be(createdReport!.Id.GetValueOrDefault());
        }

        [Fact]
        public async Task Should_Return_400()
        {
            var getReportResponseMessage = await _client.GetAsync(GetReportRequest.BuildRoute(Guid.Empty));
            getReportResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Should_Return_404()
        {
            var getReportResponseMessage = await _client.GetAsync(GetReportRequest.BuildRoute(Guid.NewGuid()));
            getReportResponseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }
}
