using BuildingBlocks.Pagination;

namespace Contact.API.Contacts.GetContact
{
    public record GetContactsQueryResult(PaginatedResult<Models.Contact> Contacts);
}
