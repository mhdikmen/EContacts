using BuildingBlocks.Dtos.ContactDtos;
using BuildingBlocks.Pagination;

namespace Contact.API.Dtos
{
    public record GetContactsDto(PaginatedResult<ContactDto> Contacts);
}
