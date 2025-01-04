using BuildingBlocks.Dtos.ContactDtos;
using BuildingBlocks.Pagination;

namespace Report.API.Dtos.ContactDtos
{
    public record GetReportsDto(PaginatedResult<ContactDto> Contacts);
}
