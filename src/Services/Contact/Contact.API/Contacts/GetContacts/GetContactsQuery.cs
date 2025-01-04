using BuildingBlocks.Pagination;

namespace Contact.API.Contacts.GetContacts;

public record GetContactsQuery : PaginationRequest, IQuery<GetContactsQueryResult>;