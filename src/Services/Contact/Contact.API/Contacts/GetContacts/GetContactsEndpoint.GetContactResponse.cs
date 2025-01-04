using BuildingBlocks.Dtos.ContactDtos;
using BuildingBlocks.Pagination;

namespace Contact.API.Contacts.GetContacts
{
    public record GetContactsResponse(PaginatedResult<ContactDto> Contacts);
}
