using BuildingBlocks.Dtos.ContactDtos;
using BuildingBlocks.Pagination;

namespace Report.API.Reports.GetReports
{
    public record GetReportsResponse(PaginatedResult<ContactDto> Contacts);
}
