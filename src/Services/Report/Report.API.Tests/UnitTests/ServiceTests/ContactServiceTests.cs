using BuildingBlocks.Dtos.ContactDtos;
using BuildingBlocks.Enums;
using BuildingBlocks.Pagination;
using Moq;
using Report.API.Dtos.ContactDtos;
using Report.API.Services.Contact;

namespace Report.API.Tests.UnitTests.ServiceTests
{
    public class ContactServiceTests
    {
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
