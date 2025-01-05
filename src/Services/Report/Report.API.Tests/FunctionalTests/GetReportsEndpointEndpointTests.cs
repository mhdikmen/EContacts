using Report.API.Reports.CreateReport;
using Report.API.Reports.GetReports;

namespace Report.API.FunctionalTests.ApiEndpointTests
{
    [Collection("Sequential")]
    public class GetReportsEndpointEndpointTests(CustomWebApplicationFactory<Report.API.Program> factory) : IClassFixture<CustomWebApplicationFactory<Report.API.Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task Should_Return_200()
        {
            var createReportRequest = new CreateReportRequest
            {
                Id = Guid.NewGuid()
            };
            var createReportRequestContent = new StringContent(JsonConvert.SerializeObject(createReportRequest), Encoding.UTF8, "application/json");
            var createReportResponseMessage = await _client.PostAsync(CreateReportRequest.BuildRoute(), createReportRequestContent);
            createReportResponseMessage.StatusCode.Should().Be(HttpStatusCode.Created);

            var getReportsResponseMessage = await _client.GetAsync(GetReportsRequest.BuildRoute(0, 1));
            getReportsResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

            var getReportsResponseContent = await getReportsResponseMessage.Content.ReadAsStringAsync();
            var getReportsResponse = JsonConvert.DeserializeObject<GetReportsResponse>(getReportsResponseContent);
            getReportsResponse.Should().NotBeNull();
            getReportsResponse!.Reports.Data.Count().Should().BeGreaterThanOrEqualTo(0);
        }

        [Fact]
        public async Task Should_Return_400()
        {
            var getContactsResponseMessage = await _client.GetAsync(GetReportsRequest.BuildRoute(0, 0));
            getContactsResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

    }
}
