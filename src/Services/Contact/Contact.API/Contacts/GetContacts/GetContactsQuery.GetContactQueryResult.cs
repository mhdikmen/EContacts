using BuildingBlocks.Pagination;

namespace Contact.API.Contacts.GetContacts
{
    public record GetContactsQueryResult(PaginatedResult<Models.Contact> Contacts);
}
