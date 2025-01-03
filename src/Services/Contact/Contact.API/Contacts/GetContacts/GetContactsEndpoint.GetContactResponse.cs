using BuildingBlocks.Dtos.ContactDtos;
using BuildingBlocks.Pagination;

namespace Contact.API.Contacts.GetContact
{
    public record GetContactsResponse(PaginatedResult<ContactDto> Contacts);
}
