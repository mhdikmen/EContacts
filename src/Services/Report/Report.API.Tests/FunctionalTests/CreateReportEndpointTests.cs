using BuildingBlocks.Dtos.ContactDtos;
using BuildingBlocks.Enums;
using BuildingBlocks.Pagination;
using Moq;
using Report.API.Dtos.ContactDtos;
using Report.API.Reports.CreateReport;
using Report.API.Services.Contact;

namespace Report.API.FunctionalTests.ApiEndpointTests
{
    [Collection("Sequential")]
    public class CreateContactDetailEndpointTests(CustomWebApplicationFactory<Report.API.Program> factory) : IClassFixture<CustomWebApplicationFactory<Report.API.Program>>
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
        public async Task Should_Return_201_WillCoverException()
        {
            var createReportRequest = new CreateReportRequest
            {
                Id = Guid.Parse("c4c1e63c-23cc-48fe-9965-70c1d75e5701")
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


        [Fact]
        public async Task GetReportsAsync_ShouldReturnContacts_WhenResponseIsSuccessful()
        {
            var response = new GetReportsDto(Contacts: new PaginatedResult<ContactDto>(0, 2, 2, 1, new List<ContactDto>
            {
                new ContactDto
                {
                    Id = Guid.NewGuid(),
                    Name = "John",
                    Surname = "Doe",
                    CompanyName = "Test",
                    CreatedDate = DateTime.UtcNow,
                    ContactDetails = new List<ContactDetailDto>
                    {
                        new ContactDetailDto
                        {
                            Id = Guid.NewGuid(),
                            Type = ContactDetailType.Location,
                            Content = "New York",
                            CreatedDate = DateTime.UtcNow,
                        }
                    }
                },
                new ContactDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Muhammed",
                    Surname = "Dikmen",
                    CompanyName = "Test",
                    CreatedDate = DateTime.UtcNow,
                    ContactDetails = new List<ContactDetailDto>
                    {
                        new ContactDetailDto
                        {
                            Id = Guid.NewGuid(),
                            Type = ContactDetailType.Location,
                            Content = "İstanbul",
                            CreatedDate = DateTime.UtcNow
                        }
                    }
                }
            }));

            var mockHandler = new MockHttpMessageHandler(JsonConvert.SerializeObject(response));
            var httpClient = new HttpClient(mockHandler)
            {
                BaseAddress = new Uri("https://localhost")
            };

            var mockFactory = new Mock<IHttpClientFactory>();
            mockFactory.Setup(f => f.CreateClient("Contact")).Returns(httpClient);

            var service = new ContactService(mockFactory.Object);

            var result = await service.GetReportsAsync();

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.First().Name.Should().Be("John");
        }
    }
}
